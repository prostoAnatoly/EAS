using Shared.Domain;

namespace Employees.Domain.ValueObjects;

public sealed class OrganizationId : StronglyTypedId
{
    public OrganizationId() : base(Guid.Empty) { }

    public OrganizationId(Guid value) : base(value) { }
}
