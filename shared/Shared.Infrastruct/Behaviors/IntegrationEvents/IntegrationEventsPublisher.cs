using Shared.App;
using Shared.IntegrationContract;

namespace Shared.Infrastructure.Behaviors.IntegrationEvents;

public class IntegrationEventsPublisher : IIntegrationEventsPublisher
{
    private readonly List<IIntegrationEvent> _events = [];

    public IEnumerable<IIntegrationEvent> Events => _events;

    public void Clear()
    {
        _events.Clear();
    }

    public void Publish(IIntegrationEvent integrationEvent)
    {
        _events.Add(integrationEvent);
    }

}