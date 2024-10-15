using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shared.Persistence;

public static class HostExtensions
{
    public static void MigrateDatabase<TContext>(this IHost host) where TContext: DbContext
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        context.Database.Migrate();
    }
}