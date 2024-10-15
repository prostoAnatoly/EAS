using Identity.Domain.Aggregates.Identities;
using Shared.Domain.Generics;

namespace Identity.Domain.Aggregates.AccessTokens;

/// <summary>
/// Токен доступа.
/// </summary>
public class AccessToken : Entity<AccessTokenId>
{

    /// <summary>
    /// Значение токена.
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Дата/время истечения срока действия токена.
    /// </summary>
    public required DateTimeOffset Expires { get; init; }

    /// <summary>
    /// Истекает, в секундах.
    /// </summary>
    public required long ExpiresIn { get; init; }

    /// <summary>
    /// ИД личности, кому принадлежал данный токен.
    /// </summary>
    public required IdentityId IdentityId { get; init; }

    /// <summary>
    /// Токен для обновления
    /// </summary>
    public required RefreshToken RefreshToken { get; init; }
}