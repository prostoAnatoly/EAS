using Shared.IntegrationContract;

namespace Shared.Infrastructure.Behaviors.IntegrationEvents;

public interface IIntegrationBus
{
    Task PublishEvent(IIntegrationEvent integrationEvent);
}