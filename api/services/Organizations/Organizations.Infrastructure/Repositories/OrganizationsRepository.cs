using Microsoft.EntityFrameworkCore;
using Organizations.App.Infrastructure.DbContexts;
using Organizations.App.Infrastructure.Repositories;
using Organizations.Domain.Aggregates.Organizations;
using Organizations.Domain.ValueObjects;

namespace Organizations.Infrastructure.Repositories;

class OrganizationsRepository : IOrganizationsRepository
{
    private readonly IOrganizationsContext _organizationsContext;

    public OrganizationsRepository(IOrganizationsContext organizationsContext)
    {
        _organizationsContext = organizationsContext;
    }

    public void AddOrganization(Organization organization)
    {
        _organizationsContext.Organizations.Add(organization);
    }

    public async Task<IEnumerable<Organization>> GetAll(UserId ownerId, CancellationToken cancellationToken = default)
    {
        return await _organizationsContext
            .Organizations
            .Where(x => x.OwnerId == ownerId)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<int> GetCountByOwner(UserId ownerId, CancellationToken cancellationToken = default)
    {
        return await _organizationsContext.Organizations.CountAsync(x => x.OwnerId == ownerId, cancellationToken);
    }
}
