using Identity.App.Infrastructure.DbContexts;
using Identity.Domain.Aggregates.AccessTokens;
using Identity.Domain.Aggregates.Identities;
using Microsoft.EntityFrameworkCore;
using SeedWork.Infrastructure.Persistence;
using Shared.Persistence;

namespace Identity.Infrastructure.Persistence;

public sealed class IdentityDatabaseContext(DbContextOptions options, IRepositoryContextOptions repositoryContextOptions)
    : BaseDatabaseContext(options, repositoryContextOptions), IIdentitiesContext, IAccessTokensContext
{

    public DbSet<IdentityInfo> Identities { get; private set; }

    public DbSet<AccessToken> AccessTokens { get; private set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
