using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Shared.Infrastructure.HealthChecks;

public static class HealthChecksExtensions
{
    public static IEndpointRouteBuilder MapDefaultHealthChecks(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", async context =>
        {
            await context.Response.WriteAsync("healthy");
        });

        return app;
    }
}
