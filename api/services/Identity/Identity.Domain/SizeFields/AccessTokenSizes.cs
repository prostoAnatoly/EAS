using Identity.Domain.Aggregates.AccessTokens;

namespace Identity.Domain.SizeFields;

/// <summary>
/// Размер полей для <see cref="AccessToken"/>
/// </summary>
public class AccessTokenSizes
{

    /// <summary>
    /// Размер значения поля <see cref="AccessToken.Value"/>
    /// </summary>
    public const int TOKEN_VALUE = 500;

}