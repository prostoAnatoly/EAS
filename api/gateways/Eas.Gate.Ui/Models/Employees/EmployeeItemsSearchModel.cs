using Eas.Gate.Ui.Models.Common.Grid;

namespace Eas.Gate.Ui.Models.Employees;

/// <summary>
/// Информация для поиска сотрудников
/// </summary>
public class EmployeeItemsSearchModel : AnyObjectItemsSearchBaseModel
{
    /// <summary>
    /// Состояние сотрудника
    /// </summary>
    public EmployeeState? State { get; set; }
}
