using System.Security.Claims;

namespace Shared.Rest.Common.Extensions;

/// <summary>
/// Расширение для типа <see cref="ClaimsIdentity"/>
/// </summary>
public static class ClaimsIdentityExtensions
{

    /// <summary>
    /// Возвращает значение утверждения
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="typeClaim">Тип утверждения</param>
    /// <returns>
    /// Возвращает значение утверждения. Если утверждения не найдено, то возвращается <code>null</code>
    /// </returns>
    public static string? GetClaimValue(this ClaimsIdentity identity, string typeClaim)
    {
        return identity.Claims.FirstOrDefault(x => x.Type == typeClaim)?.Value;
    }
}