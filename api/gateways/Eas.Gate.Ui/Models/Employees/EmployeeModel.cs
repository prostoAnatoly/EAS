using Eas.Gate.Ui.Models.Common;

namespace Eas.Gate.Ui.Models.Employees;

/// <summary>
/// Сотрудник для списка сотрудников
/// </summary>
public class EmployeeModel
{
    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public FullNameModel FullName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    /// <example>79997418511</example>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    /// <example>ivan@bmail.ru</example>
    public string Email { get; set; }

    /// <summary>
    /// Состояние сотрудника
    /// </summary>
    public EmployeeState? State { get; set; }

    /// <summary>
    /// Дата приёма на работу.
    /// </summary>
    public DateTime EmploymentDate { get; set; }
}
