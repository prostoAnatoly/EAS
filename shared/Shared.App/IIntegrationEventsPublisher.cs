using Shared.IntegrationContract;

namespace Shared.App;

public interface IIntegrationEventsPublisher
{

    void Publish(IIntegrationEvent integrationEvent);

    IEnumerable<IIntegrationEvent> Events { get; }

    void Clear();
}
