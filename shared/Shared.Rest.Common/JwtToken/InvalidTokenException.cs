namespace Shared.Rest.Common.JwtToken;

/// <summary>
/// Исключение для токена
/// </summary>
public class InvalidTokenException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="InvalidTokenException"/> с указанным сообщением об ошибке
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку</param>
    public InvalidTokenException(string message) : base(message) { }
}
