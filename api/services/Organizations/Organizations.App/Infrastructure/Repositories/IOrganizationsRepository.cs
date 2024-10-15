using Organizations.Domain.Aggregates.Organizations;
using Organizations.Domain.ValueObjects;

namespace Organizations.App.Infrastructure.Repositories;

public interface IOrganizationsRepository
{

    Task<IEnumerable<Organization>> GetAll(UserId ownerId, CancellationToken cancellationToken = default);

    Task<int> GetCountByOwner(UserId ownerId, CancellationToken cancellationToken = default);

    void AddOrganization(Organization organization);
}
