using Shared.Domain;

namespace Identity.Domain.Aggregates.Identities;

public class IdentityId : StronglyTypedId
{

    public IdentityId() : base(Guid.NewGuid()) { }

    public IdentityId(Guid value) : base(value) { }

}