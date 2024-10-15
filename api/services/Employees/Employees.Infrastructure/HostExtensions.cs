using Employees.Infrastructure.Persistence;
using Microsoft.Extensions.Hosting;
using Shared.Persistence;

namespace Employees.Infrastructure;

public static class HostExtensions
{
    public static void MigrateDatabases(this IHost host)
    {
        host.MigrateDatabase<EmployeesDatabaseContext>();
        host.MigrateDatabase<ReadModelsDatabaseContext>();
    }
}