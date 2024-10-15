using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace Shared.Rest.Common;

/// <summary>
/// Расширение типа <see cref="ControllerBase"/>
/// </summary>
public static class ControllerExtensions
{

    /// <summary>
    /// Возвращает токен доступа из заголовка <see cref="HeaderNames.Authorization"/>
    /// </summary>
    /// <param name="controller"></param>
    public static string? GetAccessTokenFromHeader(this ControllerBase controller)
    {
        return controller.ControllerContext.HttpContext.GetAccessTokenFromHeader();
    }

    /// <summary>
    /// Возвращает объект-сервис из <see cref="IServiceProvider"/>
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого объекта-сервиса</typeparam>
    /// <param name="controller"></param>
    public static T GetRequiredService<T>(this ControllerBase controller) where T : notnull
    {
        return controller.HttpContext.RequestServices.GetRequiredService<T>();
    }

    /// <summary>
    /// Возвращает объект-сервис из <see cref="IServiceProvider"/>
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого объекта-сервиса</typeparam>
    /// <param name="controller"></param>
    public static T? GetService<T>(this ControllerBase controller)
    {
        return controller.HttpContext.RequestServices.GetService<T>();
    }
}