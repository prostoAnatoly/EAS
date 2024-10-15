using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Generics;

namespace Shared.Infrastructure.Behaviors.UnitOfWork;

public class UnitOfWorkBehavior<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    private readonly DbContext _context;

    public UnitOfWorkBehavior(DbContext context)
    {
        _context = context;
    }

    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        var result = await next();

        if (request is ICommand<TOut>)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
