using MediatR;
using Shared.App;
using Shared.Mediator.Generics;

namespace Shared.Infrastructure.Behaviors.IntegrationEvents;

public class IntegrationEventsDispatchingBehavior<TIn, TOut>: IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    private readonly IIntegrationEventsPublisher _integrationEventsPublisher;
    private readonly IIntegrationBus _eventBus;

    public IntegrationEventsDispatchingBehavior(IIntegrationEventsPublisher integrationEventsPublisher, IIntegrationBus eventBus)
    {
        _integrationEventsPublisher = integrationEventsPublisher;
        _eventBus = eventBus;
    }

    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        var result = await next();

        if (request is ICommand<TOut>)
        {
            foreach (var integrationEvent in _integrationEventsPublisher.Events)
            {
                await _eventBus.PublishEvent(integrationEvent);
            }

            _integrationEventsPublisher.Clear();
        }

        return result;
    }
}