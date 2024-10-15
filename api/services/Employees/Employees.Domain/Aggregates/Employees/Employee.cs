using Employees.Domain.Aggregates.Employees.DomainEvents;
using Employees.Domain.ValueObjects;
using SeedWork.Domainn.ValueObjects;
using Shared.Common.Extensions;
using Shared.Domain.Generics;

namespace Employees.Domain.Aggregates.Employees;

/// <summary>
/// Сотрудник.
/// </summary>
public sealed class Employee : EntityBase<EmployeeId>
{
    /// <summary>
    /// Идентификатор организации.
    /// </summary>
    public OrganizationId OrganizationId { get; private set; }

    /// <summary>
    /// ФИО.
    /// </summary>
    public FullName FullName { get; private set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTimeOffset Birthday { get; private set; }

    /// <summary>
    /// Контактная информация.
    /// </summary>
    public IEnumerable<Contact>? Contacts { get; private set; }

    /// <summary>
    /// Дата приёма на работу.
    /// </summary>
    public required DateTimeOffset EmploymentDate { get; init; }

    /// <summary>
    /// Дата увольнения.
    /// </summary>
    public DateTimeOffset? DateOfDismissal { get; private set; }

    public EmployeeState State { get; private set; }

    private Employee() { }

    public static Employee Create(OrganizationId organizationId, FullName fullName, DateTimeOffset birthday,
        DateTimeOffset employmentDate, IEnumerable<Contact>? contacts = null)
    {
        var employee = new Employee
        {
            OrganizationId = organizationId,
            EmploymentDate = employmentDate,
            FullName = fullName,
            Birthday = birthday,
            Contacts = contacts,
            DateOfDismissal = null,
            State = EmployeeState.Active,
        };

        employee.AddDomainEvent(new EmployeeCreatedEvent
        {
            Employee = employee,
        });

        return employee;
    }

    public void ChangePersonalInfo(FullName newFullName, DateTimeOffset newBirthday)
    {
        if (FullName != newFullName)
        {
            FullName = newFullName;
        }
        Birthday = newBirthday;

        AddDomainEvent(new EmployeePersonalInfoChangedEvent { Employee = this });
    }

    public void ChangeContactInfo(IEnumerable<Contact>? newContacts)
    {
        if (Contacts.SequenceEqualWithNull(newContacts)) { return; }

        Contacts = newContacts;

        AddDomainEvent(new EmployeeContactInfoChangedEvent { Employee = this });
    }
}
