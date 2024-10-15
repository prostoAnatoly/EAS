using Shared.Infrastructure.Behaviors.IntegrationEvents;
using Shared.IntegrationContract;

namespace SeedWork.Infrastructure.IntegrationBuses;

public class IntegrationBusStub : IIntegrationBus
{
    public Task PublishEvent(IIntegrationEvent integrationEvent)
    {
        return Task.CompletedTask;
    }
}