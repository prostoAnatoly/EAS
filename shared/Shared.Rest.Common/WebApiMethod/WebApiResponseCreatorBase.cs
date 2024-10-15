using Shared.Rest.Common.Controllers;
using Shared.Rest.Common.Models;
using System.Reflection;

namespace Shared.Rest.Common.WebApiMethod;

/// <inheritdoc cref="IWebApiResponseCreator"/>
public abstract class WebApiResponseCreatorBase : IWebApiResponseCreator
{
    private readonly WebApiResponseInfo[] responses;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="WebApiResponseCreatorBase" />
    /// </summary>
    /// <param name="responses">Список возможных ответов</param>
    protected WebApiResponseCreatorBase(WebApiResponseInfo[] responses)
    {
        this.responses = responses;
        MethodsGeneratingRealResponse = typeof(ApiControllerBase).
        GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
    }

    /// <inheritdoc  />
    public MemberInfo[] MethodsGeneratingRealResponse { get; private set; }

    /// <summary>
    /// Возвращает тип успешного ответа по умолчанию
    /// </summary>
    protected static Type DefaultTypeSuccess { get; } = typeof(ApiResponse<>);

    /// <inheritdoc  />
    public WebApiResponseInfo? GetWebApiResponse(int statusCode)
    {
        return Array.Find(responses, x => x.StatusCode == statusCode);
    }

}