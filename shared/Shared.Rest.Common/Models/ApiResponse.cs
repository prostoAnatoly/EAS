namespace Shared.Rest.Common.Models;

/// <summary>
/// Ответ от веб-методов в обобщённой форме
/// </summary>
/// <typeparam name="TPayload">Тип полезной нагрузки в ответе от сервера</typeparam>
public class ApiResponse<TPayload>
    where TPayload: class
{

    /// <summary>
    /// Данные от веб-метода при формировании ответа сервера
    /// </summary>
    public TPayload Payload { get; }

    public ApiResponse(TPayload payload)
    {
        Payload = payload;
    }
}