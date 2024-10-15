using SeedWork.Domain;

namespace Employees.Domain.Aggregates.Employees.DomainEvents;

public sealed class EmployeeCreatedEvent : DomainBaseEvent
{
    public required Employee Employee { get; init; }
}
