using System.Linq.Expressions;

namespace Shared.Common.Models.TableFilters;

/// <summary>
/// Информация о колонке для сортировки и фильтрации
/// </summary>
public class ColumnInfo<TSource>
{
    /// <summary>
    /// Функция фильтрации
    /// </summary>
    public required Func<IQueryable<TSource>, string, IQueryable<TSource>>? Filtering { get; set; }

    /// <summary>
    /// Имя колонки
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Выражение для сортировки
    /// </summary>
    public required Expression<Func<TSource, object>>? Sorting { get; set; }
}