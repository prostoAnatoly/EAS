using Employees.Domain.Aggregates.Employees;
using Shared.Mediator.Generics;

namespace Employees.App.Features;

public sealed record CreateEmployeeCommand : ICommand<EmployeeId>
{
    public required Guid OrganizationId { get; init; }

    public required string Name { get; init; }

    public required string Surname { get; init; }

    public string? Patronymic { get; init; }

    public string? MobilePhoneNumber { get; init; }

    public string? Email { get; init; }

    public required DateTimeOffset Birthday { get; init; }

    public required DateTimeOffset EmploymentDate { get; init; }

}