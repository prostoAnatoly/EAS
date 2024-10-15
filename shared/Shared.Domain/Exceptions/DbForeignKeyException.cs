namespace Shared.Domain.Exceptions;

/// <summary>
/// Исключение, генерируемое при работе с БД в случае ошибки, которая вызвана ограничением внешнего ключа
/// </summary>
/// <remarks>
/// Инициализирует новый экземпляр класса <see cref="DbForeignKeyException"/>
/// </remarks>
/// <param name="message">Сообщение, описывающее ошибку</param>
/// <param name="innerException">Исключение, которое является причиной текущего исключения</param>
public sealed class DbForeignKeyException(string message, Exception? innerException) : Exception(message, innerException)
{

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DbForeignKeyException"/>
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку</param>
    public DbForeignKeyException(string message) : this(message, null) { }
}