namespace Shared.Rest.Common.Models;

/// <summary>
/// Ответ сервера при ошибке, произошедшей на сервере
/// </summary>
public class ResponseErrorModelBase
{

    /// <summary>
    /// Основное сообщение, описывающее ошибку
    /// </summary>
    /// <example>Текст ошибки</example>
    public string MessageBase { get; set; }

}