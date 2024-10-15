using SeedWork.Domain;

namespace Organizations.Domain.Aggregates.Organizations.DomainEvents;

public class OrganizationCreatedEvent : DomainBaseEvent
{

    public required OrganizationId OrganizationId { get; init; }
}
