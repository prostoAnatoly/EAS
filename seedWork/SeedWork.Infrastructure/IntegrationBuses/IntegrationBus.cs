using MassTransit;
using Shared.Infrastructure.Behaviors.IntegrationEvents;
using Shared.IntegrationContract;

namespace SeedWork.Infrastructure.IntegrationBuses;

class IntegrationBus : IIntegrationBus
{
    private readonly IBus _bus;

    public IntegrationBus(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishEvent(IIntegrationEvent integrationEvent)
    {
        await _bus.Publish(integrationEvent);
    }
}
