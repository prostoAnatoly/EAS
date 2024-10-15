using System.Reflection;

namespace Shared.Rest.Common.WebApiMethod;

/// <summary>
/// Создатель ответов для веб-API
/// </summary>
public interface IWebApiResponseCreator
{

    /// <summary>
    /// Возвращает информацию об ответе по коду состояния HTTP
    /// </summary>
    /// <param name="statusCode">Код состояния HTTP</param>
    WebApiResponseInfo? GetWebApiResponse(int statusCode);

    /// <summary>
    /// Список методов, генерирующие реальные HTTP-ответы
    /// </summary>
    MemberInfo[] MethodsGeneratingRealResponse { get; }
}