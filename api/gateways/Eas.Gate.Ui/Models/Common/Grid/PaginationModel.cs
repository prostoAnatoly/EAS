namespace Eas.Gate.Ui.Models.Common.Grid;

public class PaginationModel<T> where T : class
{
    /// <summary>
    /// Элементы списка
    /// </summary>
    public T[] Items { get; set; }

    /// <summary>
    /// Кол-во страниц
    /// </summary>
    /// <example>3</example>
    public int TotalPages { get; set; }

    /// <summary>
    /// Общее кол-во записей в выборке
    /// </summary>
    /// <example>15</example>
    public int Total { get; set; }
}
