using Shared.Common.Models.Paging;

namespace Shared.Common.Models.TableFilters;

/// <summary>
/// Информация для сортировки в табличном виде
/// </summary>
public sealed record ColumnSort
{
    private string _columnName = string.Empty;

    /// <summary>
    /// Тип сортировки
    /// </summary>
    /// <example>0</example>
    public SortType SortType { get; set; }

    /// <summary>
    /// Имя колонки сортировки
    /// </summary>
    /// <example>DateTimeEvent</example>
    public required string ColumnName
    {
        get => _columnName;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(ColumnName));
            }
            _columnName = value;
        }
    }
}