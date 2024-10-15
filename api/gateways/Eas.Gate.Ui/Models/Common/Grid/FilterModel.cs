namespace Eas.Gate.Ui.Models.Common.Grid;

public class FilterModel
{
    /// <summary>
    /// Значение фильтра
    /// </summary>
    /// <example>Поисковая строка</example>
    public string Value { get; set; }


    /// <summary>
    /// Имя колонки фильтрации
    /// </summary>
    /// <example>Info</example>
    public string ColumnName { get; set; }
}
