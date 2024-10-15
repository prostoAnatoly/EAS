using Employees.App.ReadModel.DbContexts;
using Employees.App.ReadModel.Models;
using Employees.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using SeedWork.Infrastructure.Persistence;
using Shared.Persistence;

namespace Employees.Infrastructure.Persistence;

sealed class ReadModelsDatabaseContext : BaseDatabaseContext, IReadModelsContext
{
    public ReadModelsDatabaseContext(DbContextOptions<ReadModelsDatabaseContext> options,
        IRepositoryContextOptions repositoryContextOptions) : base(options, repositoryContextOptions) { }

    public DbSet<EmployeeRm> EmployeeRms { get; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ReadModelsConfiguration());

        base.OnModelCreating(modelBuilder);
    }

}
