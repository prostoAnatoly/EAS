namespace SeedWork.Infrastructure.IntegrationBuses;

public class IntegrationBusOptions
{
    public bool Enabled { get; init; } = true;

    public string Host { get; init; }

    public string VirtualHost { get; init; } = "/";

    public string Username { get; init; }

    public string Password { get; init; }

    public string ReceiveEndpoint { get; init; }

    /// <summary>
    /// Количество повторных попыток.
    /// </summary>
    public int RetryCount { get; init; } = 0;

    /// <summary>
    /// Первичный интервал в мс для повтора.
    /// </summary>
    public int RetryInitialIntervalMs { get; init; } = 200;

    /// <summary>
    /// Прирост в интервале в мс между повторами.
    /// </summary>
    public int RetryIntervalIncrementMs { get; init; } = 250;

}
