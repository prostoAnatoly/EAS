using System.Security.Claims;

namespace Identity.App.Infrastructure.Services;

/// <summary>
/// Функции по работе с утверждениями для JWT
/// </summary>
public interface IJwtTokenClaimService
{

    /// <summary>
    /// Ключ для информации "IP-адрес клиента" в утверждение
    /// </summary>
    public string ClaimNameIpAddress { get; }

    /// <summary>
    /// Ключ для информации "идентификационная строка клиентского приложения" в утверждение
    /// </summary>
    public string ClaimNameUserAgent { get; }

    /// <summary>
    /// Создаёт утверждение для IP-адреса клиента
    /// </summary>
    /// <param name="ipAddress">IP-адрес клиента</param>
    /// <returns>Возвращает утверждение для IP-адреса клиента</returns>
    Claim CreateClaimByIpAddress(string ipAddress);

    /// <summary>
    /// Создаёт утверждение для идентификационной строки клиентского приложения
    /// </summary>
    /// <param name="userAgent">Идентификационная строка клиентского приложения</param>
    /// <returns>Возвращает утверждение для идентификационной строки клиентского приложения</returns>
    Claim CreateClaimByUserAgent(string userAgent);

    /// <summary>
    /// Возвращает IP-адрес клиента
    /// </summary>
    /// <param name="principal"></param>
    string? GetIpAddress(ClaimsIdentity identity);

    /// <summary>
    /// Возвращает идентификационную строку клиентского приложения
    /// </summary>
    /// <param name="identity"></param>
    string? GetUserAgent(ClaimsIdentity identity);
}