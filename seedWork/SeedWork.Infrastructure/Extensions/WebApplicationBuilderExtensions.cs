using Microsoft.AspNetCore.Builder;

namespace SeedWork.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions
{

    public static string GetEndPointValue(this WebApplicationBuilder webApplicationBuilder, string endpointName)
    {
        var value = webApplicationBuilder.Configuration[endpointName]
            ?? throw new ArgumentException(message: $"Не задано значение конечной точки: {endpointName}",
                paramName: nameof(endpointName));

        return value;
    }
}
