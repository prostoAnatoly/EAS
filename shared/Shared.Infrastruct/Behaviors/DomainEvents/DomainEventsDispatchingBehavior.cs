using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Mediator.Generics;

namespace Shared.Infrastructure.Behaviors.DomainEvents;

sealed class DomainEventsDispatchingBehavior<TIn, TOut>: IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    private readonly IMediator _mediator;
    private readonly DbContext _context;

    public DomainEventsDispatchingBehavior(IMediator mediator, DbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        var result = await next();

        if (request is ICommand<TOut>)
        {
            await DispatchDomainEvents(cancellationToken);
        }

        return result;
    }

    private async Task DispatchDomainEvents(CancellationToken cancellationToken)
    {
        var entries = _context.ChangeTracker.Entries<Entity>();
        if (entries.Any(a => a.Entity.HasEvents))
        {
            foreach (var entity in entries
                                    .Where(a => a.Entity.HasEvents)
                                    .Select(a => a.Entity))
            {
                foreach (var domainEvent in entity.DomainEvents)
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }

                entity.ClearDomainEvents();
            }
        }
    }
}