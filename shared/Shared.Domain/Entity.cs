namespace Shared.Domain;

public abstract class Entity
{
    private List<IDomainEvent>? domainEvents;
    private static readonly IEnumerable<IDomainEvent> emptyDomainEvents = Enumerable.Empty<IDomainEvent>();

    /// <summary>
    /// Массив событий предметной области.
    /// </summary>
    public IEnumerable<IDomainEvent> DomainEvents => domainEvents ?? emptyDomainEvents;

    /// <summary>
    /// Добавление события предметной области.
    /// </summary>
    /// <param name="domainEvent">Событие предметной области.</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        var events = GetDomainEvents();
        
        events.Add(domainEvent);
    }

    /// <summary>
    /// Очистка событий предметной области.
    /// </summary>
    public void ClearDomainEvents()
    {
        domainEvents?.Clear();
    }

    public bool HasEvents => domainEvents != null && domainEvents.Count > 0;

    private IList<IDomainEvent> GetDomainEvents()
    {
        return domainEvents ??= [];
    }

}