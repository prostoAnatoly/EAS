using Employees.App.Infrastructure.DbContexts;
using Employees.Domain.Aggregates.Employees;
using Employees.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using SeedWork.Infrastructure.Persistence;
using Shared.Persistence;

namespace Employees.Infrastructure.Persistence;

sealed class EmployeesDatabaseContext : BaseDatabaseContext, IEmployeesContext
{
    public EmployeesDatabaseContext(DbContextOptions<EmployeesDatabaseContext> options,
        IRepositoryContextOptions repositoryContextOptions) : base(options, repositoryContextOptions) { }

    public DbSet<Employee> Employees { get; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

}
