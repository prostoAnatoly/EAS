namespace Eas.Gate.Ui.Models.Common.Grid;

/// <summary>
/// Информация для поиска объектов для табличного представления
/// </summary>
public abstract class AnyObjectItemsSearchBaseModel
{
    /// <summary>
    /// Размер страницы
    /// </summary>
    /// <example>10</example>
    public int PageSize { get; set; }

    /// <summary>
    /// Номер страницы
    /// </summary>
    /// <example>3</example>
    public int Page { get; set; }

    /// <summary>
    /// Информация о сортировке
    /// </summary>
    public SortModel Sort { get; set; }

    /// <summary>
    /// Информация о фильтрации
    /// </summary>
    public FilterModel[] Filters { get; set; }
}
