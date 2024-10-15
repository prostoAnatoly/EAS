using Shared.Domain;

namespace Organizations.Domain.Aggregates.Organizations;

public class OrganizationId : StronglyTypedId
{
    public OrganizationId() : base(Guid.Empty) { }

    public OrganizationId(Guid value) : base(value) { }
}
