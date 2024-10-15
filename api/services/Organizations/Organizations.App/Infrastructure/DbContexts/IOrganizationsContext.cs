using Microsoft.EntityFrameworkCore;
using Organizations.Domain.Aggregates.Organizations;

namespace Organizations.App.Infrastructure.DbContexts;

public interface IOrganizationsContext
{

    public DbSet<Organization> Organizations { get; }
}
