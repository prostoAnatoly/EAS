using Shared.Domain;

namespace Identity.Domain.Aggregates.AccessTokens;

public sealed class AccessTokenId : StronglyTypedId
{

    public AccessTokenId() : base(Guid.NewGuid()) { }

    public AccessTokenId(Guid value) : base(value) { }
}