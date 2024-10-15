using Identity.Domain.Aggregates.Identities;

namespace Identity.Domain.SizeFields;

/// <summary>
/// Размер полей для <see cref="IdentityInfo"/>
/// </summary>
public class IdentityInfoSizes
{

    /// <summary>
    /// Размер значения поля <see cref="IdentityInfo.UserName"/>
    /// </summary>
    public const int USER_NAME = 30;

    /// <summary>
    /// Размер значения поля <see cref="IdentityInfo.Password"/>
    /// </summary>
    public const int PASSWORD = 64;

}