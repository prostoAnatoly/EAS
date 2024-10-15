namespace Shared.Common.Models.TableFilters;

/// <summary>
/// Информация для фильтрации данных в табличном виде
/// </summary>
public sealed record ColumnFilter
{
    private string _columnName = string.Empty;

    /// <summary>
    /// Значение фильтра
    /// </summary>
    /// <example>Код теста</example>
    public string? Value { get; set; }

    /// <summary>
    /// Имя колонки фильтрации
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