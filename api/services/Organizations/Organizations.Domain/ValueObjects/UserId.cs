using Shared.Domain;

namespace Organizations.Domain.ValueObjects;

public class UserId : StronglyTypedId
{
    public UserId() : base(Guid.Empty) { }

    public UserId(Guid value) : base(value) { }
}
