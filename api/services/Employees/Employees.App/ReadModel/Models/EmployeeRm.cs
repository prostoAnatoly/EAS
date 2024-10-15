using Employees.Domain.Aggregates.Employees;
using SeedWork.Application;

namespace Employees.App.ReadModel.Models;

public sealed class EmployeeRm : IReadModelWithId
{
    public required Guid OrganizationId { get; init; }

    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public required string Surname { get; set; }

    public string? Patronymic { get; set; }

    public required string? Email { get; set; }

    public required EmployeeState State { get; set; }
}
