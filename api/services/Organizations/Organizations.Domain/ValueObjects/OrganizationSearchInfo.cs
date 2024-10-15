using Shared.Domain;

namespace Organizations.Domain.ValueObjects;

public class OrganizationSearchInfo : ValueObject
{
    public required string Name { get; init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
    }
}
