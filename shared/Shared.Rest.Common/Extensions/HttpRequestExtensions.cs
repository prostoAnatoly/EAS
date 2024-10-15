using Microsoft.AspNetCore.Http;
using Shared.Common.Utils;

namespace Shared.Rest.Common;

/// <summary>
/// Расширение для типа <see cref="HttpRequest"/>
/// </summary>
public static class HttpRequestExtensions
{

    /// <summary>
    /// Возвращает IP-адрес удалённого целевого объекта
    /// </summary>
    /// <param name="httpRequest"></param>
    public static string GetIpAddress(this HttpRequest httpRequest)
    {
        return httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Возвращает значение заголовка
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="header"></param>
    public  static string GetHeaderValue(this HttpRequest httpRequest, string header)
    {
        return httpRequest.Headers.ContainsKey(header) ?
                    httpRequest.Headers[header].ToString() : string.Empty;
    }

    /// <summary>
    /// Возвращает имя контроллера
    /// </summary>
    /// <param name="httpRequest"></param>
    public static string? GetControllerName(this HttpRequest httpRequest)
    {
        return httpRequest.RouteValues["controller"]?.ToString();
    }

    /// <summary>
    /// Возвращает имя действия контроллера
    /// </summary>
    /// <param name="httpRequest"></param>
    public static string? GetActionName(this HttpRequest httpRequest)
    {
        return httpRequest.RouteValues["action"]?.ToString();
    }

    /// <summary>
    /// Возвращает базовый адрес
    /// </summary>
    /// <param name="httpRequest"></param>
    public static string GetBaseAddess(this HttpRequest httpRequest)
    {
        return NetUtils.GetBaseAddess(httpRequest.Scheme, httpRequest.Host.Value);
    }

}