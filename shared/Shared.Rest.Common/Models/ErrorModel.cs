namespace Shared.Rest.Common.Models;

/// <summary>
/// Информация об ошибке
/// </summary>
public class ErrorModel
{

    /// <summary>
    /// Массив сообщений об ошибке
    /// </summary>
    /// <example>{Ошибка1, Ошибка2, Ошибка3}</example>
    public string[] Messages { get; set; }
}