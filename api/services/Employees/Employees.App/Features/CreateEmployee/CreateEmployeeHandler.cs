using Employees.Domain.Aggregates.Employees;
using Employees.Domain.ValueObjects;
using SeedWork.Domainn.ValueObjects;
using Shared.Mediator;

namespace Employees.App.Features;

sealed class CreateEmployeeHandlerHandler : ICommandHandler<CreateEmployeeCommand, EmployeeId>
{
    private readonly IEmployeesRepository _employeesRepository;

    public CreateEmployeeHandlerHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public Task<EmployeeId> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var organizationId = new OrganizationId(request.OrganizationId);
        var fullName = new FullName
        {
            Name = request.Name,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
        };
        var contacts = CreateContacts(request);

        var employee = Employee.Create(organizationId, fullName, request.Birthday, request.EmploymentDate, contacts);

        _employeesRepository.AddEmployee(employee);

        return Task.FromResult(employee.Id);
    }

    private static IEnumerable<Contact>? CreateContacts(CreateEmployeeCommand request)
    {
        if (string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.MobilePhoneNumber))
        {
            yield break;
        }

        if (!string.IsNullOrEmpty(request.MobilePhoneNumber))
        {
            yield return new Contact { Type = ContactType.MobilePhoneNumber, Value = request.MobilePhoneNumber };
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            yield return new Contact { Type = ContactType.MobilePhoneNumber, Value = request.Email };
        }
    }
}
