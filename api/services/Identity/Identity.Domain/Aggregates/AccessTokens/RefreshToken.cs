using Shared.Domain;

namespace Identity.Domain.Aggregates.AccessTokens;

/// <summary>
/// Токен для обновления токена доступа - <see cref="AccessToken"/>
/// </summary>
public class RefreshToken : ValueObject
{

    /// <summary>
    /// Значение токена
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Дата/время истечения срока действия токена
    /// </summary>
    public required DateTimeOffset Expires { get; init; }

    /// <summary>
    /// Дата/время создания токена
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// IP-адрес устройства для которого создан токен
    /// </summary>
    public required string CreatedByIp { get; init; }

    /// <summary>
    /// Цифровой опечаток устройства и браузера для которого создан токен
    /// </summary>
    public required string Fingerprint { get; init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}