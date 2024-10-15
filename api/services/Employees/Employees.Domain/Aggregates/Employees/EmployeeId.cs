using Shared.Domain;

namespace Employees.Domain.Aggregates.Employees;

public sealed class EmployeeId : StronglyTypedId
{

    public EmployeeId() : base(Guid.NewGuid()) { }

    public EmployeeId(Guid value) : base(value) { }
}
