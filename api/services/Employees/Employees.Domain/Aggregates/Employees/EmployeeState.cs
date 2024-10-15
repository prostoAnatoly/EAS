namespace Employees.Domain.Aggregates.Employees;

public enum EmployeeState
{

    None = 0,

    /// <summary>
    /// Действующий
    /// </summary>
    Active = 1,

    /// <summary>
    /// Уволенный
    /// </summary>
    Dismissed = 2,
}
