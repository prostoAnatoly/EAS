using Identity.Domain.Aggregates.AccessTokens;

namespace Identity.Domain.SizeFields;

/// <summary>
/// Размер полей для <see cref="RefreshToken"/>
/// </summary>
public class RefreshTokenSizes
{

    /// <summary>
    /// Размер значения поля <see cref="RefreshToken.Value"/>
    /// </summary>
    public const int TOKEN_VALUE = 500;

    /// <summary>
    /// Размер значения поля <see cref="RefreshToken.CreatedByIp"/>
    /// </summary>
    public const int CREATED_BY_IP = CommonSizes.IPv4;

    /// <summary>
    /// Размер значения поля <see cref="RefreshToken.Fingerprint"/>
    /// </summary>
    public const int FINGERPRINT = 4000;
}