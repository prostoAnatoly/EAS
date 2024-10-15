namespace Eas.Gate.Ui.Models.Common.Grid;

public class SortModel
{
    /// <summary>
    /// Тип сортировки
    /// </summary>
    public SortType SortType { get; set; }

    /// <summary>
    /// Имя колонки сортировки
    /// </summary>
    /// <example>DateTimeEvent</example>
    public string OrderBy { get; set; }
}
