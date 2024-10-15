namespace Shared.Rest.Common.Models;

/// <summary>
/// Ответ сервера при ошибке, произошедшей на сервере
/// </summary>
public class ResponseErrorBadRequest : ResponseErrorModelBase
{

    /// <summary>
    /// Массив ошибок
    /// </summary>
    public Dictionary<string, ErrorModel> Errors { get; set; }

}