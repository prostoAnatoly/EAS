using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Shared.Rest.Common;

/// <summary>
/// Расширение для типа <see cref="HttpContext"/>
/// </summary>
public static class HttpContextExtensions
{


    /// <summary>
    /// Возвращает значение заголовка
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="header"></param>
    public static string GetHeaderValue(this HttpContext httpContext, string header)
    {
        return httpContext.Request.Headers.TryGetValue(header, out StringValues value)
            ? value.ToString()
            : string.Empty;
    }

    /// <summary>
    /// Возвращает IP-адрес удалённого целевого объекта
    /// </summary>
    /// <param name="httpContext"></param>
    public static string GetIpAddress(this HttpContext httpContext)
    {
        return httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Возвращает токен доступа из заголовка <see cref="HeaderNames.Authorization"/>
    /// </summary>
    /// <param name="httpContext"></param>
    public static string? GetAccessTokenFromHeader(this HttpContext httpContext)
    {
        var authHeader = httpContext.Request.GetHeaderValue(HeaderNames.Authorization);
        var lenAuthScheme = JwtBearerDefaults.AuthenticationScheme.Length + 1;
        if (!string.IsNullOrEmpty(authHeader) && authHeader.Length > lenAuthScheme)
        {
            return authHeader[lenAuthScheme..];
        }
        return null;
    }

    /// <summary>
    /// Возвращает базовый адрес
    /// </summary>
    /// <param name="httpContext"></param>
    public static string GetBaseAddess(this HttpContext httpContext)
    {
        return httpContext.Request.GetBaseAddess();
    }
}