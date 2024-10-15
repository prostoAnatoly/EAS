using Microsoft.EntityFrameworkCore;

namespace Shared.Persistence;

public interface IRepositoryContextOptions
{

    /// <summary>
    /// Возвращает индикатор, показывающий, что возникшее исключение вызвано ограничением внешнего ключа
    /// </summary>
    /// <param name="dbUpdateException">Исключение, возникшее при сохранении данных в БД. <see cref="DbUpdateException"/></param>
    /// <returns>
    /// <see langword="true" /> если исключение вызвано ограничением внешнего ключа; иначе <see langword="false" />
    /// </returns>
    bool IsDbForeingKeyException(DbUpdateException dbUpdateException);
}
