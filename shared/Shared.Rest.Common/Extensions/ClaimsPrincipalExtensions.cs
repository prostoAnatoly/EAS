using System.Security.Claims;

namespace Shared.Rest.Common.Extensions;

/// <summary>
/// Расширение для типа <see cref="ClaimsPrincipal"/>
/// </summary>
public static class ClaimsPrincipalExtensions
{

    /// <summary>
    /// Возвращает значение утверждения
    /// </summary>
    /// <param name="principal"></param>
    /// <param name="typeClaim">Тип утверждения</param>
    /// <returns>
    /// Возвращает значение утверждения. Если утверждения не найдено, то возвращается <code>null</code>
    /// </returns>
    public static string? GetClaimValue(this ClaimsPrincipal principal, string typeClaim)
    {
        return principal.Claims.FirstOrDefault(x => x.Type == typeClaim)?.Value;
    }

    /// <summary>
    /// Удалить специфические утверждения из всех <see cref="ClaimsIdentity"/>
    /// </summary>
    /// <param name="principal"></param>
    /// <param name="policyEnum">Имя политики и тип утверждения</param>
    /// <param name="prefixByName">Приставка к имени перечисления переданного в параметре <paramref name="policyEnum"/></param>
    public static void RemoveClaims(this ClaimsPrincipal principal, Type policyEnum, 
        string? prefixByName = null)
    {
        foreach(var identity in principal.Identities)
        {
            foreach (var name in Enum.GetNames(policyEnum))
            {
                if (string.IsNullOrEmpty(prefixByName) || name.StartsWith(prefixByName))
                {
                    var claim = identity.FindFirst(name);
                    if (claim != null)
                    {
                        identity.RemoveClaim(claim);
                    }
                }
            }
        }
    }
}