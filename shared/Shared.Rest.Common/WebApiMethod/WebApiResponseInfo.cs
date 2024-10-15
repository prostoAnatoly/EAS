namespace Shared.Rest.Common.WebApiMethod;

/// <summary>
/// Инфомрация об ответе веб-API
/// </summary>
public class WebApiResponseInfo
{

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="WebApiResponseInfo"/>
    /// </summary>
    public WebApiResponseInfo()
    {
        ResponseCodes = new List<Enum>();
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="WebApiResponseInfo"/>
    /// </summary>
    /// <param name="statusCode">Код состояния HTTP</param>
    /// <param name="responseType">Тип ответа</param>
    public WebApiResponseInfo(int statusCode, Type responseType)
    {
        StatusCode = statusCode;
        ResponseCodes = new List<Enum>();
        ResponseType = responseType;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="WebApiResponseInfo"/>
    /// </summary>
    /// <param name="statusCode">Код состояния HTTP</param>
    /// <param name="responseCodes">Список возможных возвращаемых кодов ответа</param>
    /// <param name="responseType">Тип ответа</param>
    public WebApiResponseInfo(int statusCode, Enum[] responseCodes, Type responseType)
    {
        StatusCode = statusCode;
        ResponseCodes = new List<Enum>(responseCodes);
        ResponseType = responseType;
    }

    /// <summary>
    /// Код состояния HTTP
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Список возможных возвращаемых кодов ответа
    /// </summary>
    public IList<Enum> ResponseCodes { get; set; }

    /// <summary>
    /// Тип ответа
    /// </summary>
    public Type ResponseType { get; set; }

    /// <summary>
    /// Объект-пример
    /// </summary>
    public object Example { get; set; }

    /// <summary>
    /// Типы аргументов в обобщённом типа указанного в <see cref="ResponseType"/>
    /// </summary>
    public Type[] GenericArgumentZeroType { get; set; }

    /// <summary>
    /// Индикатор, показывающий что тип ответа один на всю систему
    /// </summary>
    public bool IsGlobal { get; set; }
}