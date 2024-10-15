using Microsoft.EntityFrameworkCore;
using Organizations.App.Infrastructure.DbContexts;
using Organizations.Domain.Aggregates.Organizations;
using SeedWork.Infrastructure.Persistence;
using Shared.Persistence;

namespace Organizations.Infrastructure.Persistence;

public sealed class OrganizationsDatabaseContext(DbContextOptions options, IRepositoryContextOptions repositoryContextOptions)
    : BaseDatabaseContext(options, repositoryContextOptions), IOrganizationsContext
{
    public DbSet<Organization> Organizations { get; private set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationsDatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
