using Employees.Domain.Aggregates.Employees;

namespace Employees.App.Dtos;

public sealed class FilterByEmployeeDto
{

    public EmployeeState State { get; set; }
}
