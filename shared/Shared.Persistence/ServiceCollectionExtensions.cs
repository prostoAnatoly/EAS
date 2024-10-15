using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDbContext<TContext>(this IServiceCollection services,
        Action<DbContextOptionsBuilder> setUseSql) where TContext : DbContext
    {
        services
            .AddDbContext<TContext>(builder => setUseSql(builder))
            .AddScoped<DbContext>(a => a.GetRequiredService<TContext>());

        return services;
    }

    public static IServiceCollection AddTestDatabaseContext<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services
            .AddDbContext<TContext>(builder => builder.UseInMemoryDatabase("TestDatabase"))
            .AddScoped<DbContext>(a => a.GetRequiredService<TContext>());
        
        return services;
    }
}