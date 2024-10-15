using Shared.Common.Models.TableFilters;
using System.Linq.Expressions;

namespace Shared.Common.Extensions;

public static class QueryableExtensions
{

    public static IQueryable<T> Paging<T>(this IQueryable<T> source, int offset, int count)
    {
        return source.Skip(offset).Take(count);
    }

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition
            ? source.Where(predicate)
            : source;
    }

    /// <summary>
    /// Сортировка
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="sortType">Тип сортировки</param>
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, Expression<Func<T, object>> keySelector,
        SortType sortType)
    {
        return sortType == SortType.Asc ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
    }

    /// <summary>
    /// Сортировка
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="sortType">Тип сортировки</param>
    public static IQueryable<T> ThenByDynamic<T>(this IOrderedQueryable<T> source, Expression<Func<T, object>> keySelector,
        SortType sortType)
    {
        return sortType == SortType.Asc ? source.ThenBy(keySelector) : source.ThenByDescending(keySelector);
    }

    /// <summary>
    /// Сортировка
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <param name="sortType">Тип сортировки</param>
    public static IQueryable<T> ThenByDynamic<T>(this IQueryable<T> source, Expression<Func<T, object>> keySelector,
        SortType sortType)
    {
        if (source is IOrderedQueryable<T> ordered)
        {
            return ordered.ThenByDynamic(keySelector, sortType);
        }

        return source;
    }

    /// <summary>
    /// Добавляет сортировку по умолчанию
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">Первоначальный запрос</param>
    /// <param name="isOrderedBy">Есть ли сортировка</param>
    /// <param name="keySelector">Выбор поля для сортировки</param>
    /// <returns></returns>
    public static IQueryable<T> AddOrderByDefault<T>(this IQueryable<T> source, bool isOrderedBy, Expression<Func<T, object>> keySelector)
    {
        if (isOrderedBy)
        {
            var q = (IOrderedQueryable<T>)source;

            return q.ThenBy(keySelector);
        }
        else
        {
            return source.OrderBy(keySelector);
        }
    }

    public static IQueryable<T> Filtration<T>(this IQueryable<T> source, ColumnInfo<T>[]? columns,
        IEnumerable<ColumnFilter>? filters)
    {
        if (columns == null || filters == null)
        {
            return source;
        }

        foreach (var columnFilter in filters)
        {
            if (columnFilter == null || string.IsNullOrEmpty(columnFilter.ColumnName) || string.IsNullOrEmpty(columnFilter.Value))
            {
                continue;
            }

            foreach (var column in columns
                                    .Where(x => x.Name.Equals(columnFilter.ColumnName, StringComparison.InvariantCultureIgnoreCase))
                                    .Where(x => x.Filtering != null))
            {
                source = column.Filtering!(source, columnFilter.Value);
            }
        }

        return source;
    }

    public static IQueryable<T> Sorting<T>(this IQueryable<T> source, ColumnInfo<T>[]? columns,
        IEnumerable<ColumnSort>? sorts, Expression<Func<T, object>>? defaultOrder)
    {
        if (columns == null || sorts == null)
        {
            if (defaultOrder != null)
            {
                return source.OrderBy(defaultOrder);
            }

            return source;
        }

        var counter = 0;
        foreach (var sort in sorts)
        {
            if (sort == null || string.IsNullOrEmpty(sort.ColumnName))
            {
                continue;
            }

            foreach (var sorting in columns
                    .Where(x => x.Name.Equals(sort.ColumnName, StringComparison.InvariantCultureIgnoreCase))
                    .Where(x => x.Sorting != null)
                    .Select(x => x.Sorting!))
            {
                if (counter == 0)
                {
                    source = source.OrderByDynamic(sorting, sort.SortType);
                }
                else
                {
                    source = source.ThenByDynamic(sorting, sort.SortType);
                }
                counter++;
            }
        }

        if (counter == 0 && defaultOrder != null)
        {
            return source.OrderBy(defaultOrder);
        }

        return source;
    }
}
