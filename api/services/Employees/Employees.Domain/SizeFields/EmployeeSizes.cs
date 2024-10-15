using Employees.Domain.Aggregates.Employees;

namespace Employees.Domain.SizeFields;

/// <summary>
/// Размер полей для <see cref="Employee"/>
/// </summary>
public class EmployeeSizes
{

    /// <summary>
    /// Размер значения поля <see cref="Employee.Email"/>
    /// </summary>
    public const int EMAIL = 255;

    /// <summary>
    /// Размер значения поля <see cref="Employee.PhoneNumber"/>
    /// </summary>
    public const int PHONE_NUMBER = 11;

    public const int CONTACT_VALUE = 255;
}