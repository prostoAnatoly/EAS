using Organizations.Domain.Aggregates.Organizations.DomainEvents;
using Organizations.Domain.ValueObjects;
using Shared.Domain.Generics;

namespace Organizations.Domain.Aggregates.Organizations;

public class Organization : EntityBase<OrganizationId>
{

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Идентификатор создателя.
    /// </summary>
    public required UserId CreatorId {  get; init; }

    /// <summary>
    /// Идентификатор владельца.
    /// </summary>
    public UserId OwnerId { get; private set; } = null!;

    public OrganizationSearchInfo SearchInfo { get; private set; } = null!;

    private Organization() { }

    public static Organization Create(string name, UserId ownerId, UserId creatorId)
    {
        var organization = new Organization
        {
            CreatorId = creatorId,
            Name = name,
            OwnerId = ownerId,
        };

        organization.UpdateSearchInfo();

        organization.AddDomainEvent(new OrganizationCreatedEvent { OrganizationId = organization.Id });

        return organization;
    }

    private void UpdateSearchInfo()
    {
        SearchInfo = new OrganizationSearchInfo
        {
            Name = Name.ToLowerInvariant(),
        };

    }

    public void ChangeName(string newName)
    {
        if (Name.Equals(newName, StringComparison.InvariantCultureIgnoreCase)) { return; }

        Name = newName;
        SearchInfo = new OrganizationSearchInfo
        {
            Name = newName.ToLowerInvariant(),
        };
    }
}
