using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SeedWork.Infrastructure.Persistence.RepositoryContextOptions;
using Shared.Persistence;

namespace SeedWork.Persistence;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddDefaultDbContext<TContext>(this IServiceCollection services,
        IDbContextOptions dbContextOptions) where TContext: DbContext
    {
        services
            .AddCustomDbContext<TContext>(builder =>
                builder
                .UseNpgsql(dbContextOptions.DatabaseConnectionString)
                .UseSnakeCaseNamingConvention());

        services.AddSingleton<IRepositoryContextOptions, RepositoryContextOptionsPostgreSql>();

        return services;
    }
}