using Employees.App.ReadModel.DbContexts;
using Employees.App.ReadModel.Models;
using Employees.Domain.Aggregates.Employees.DomainEvents;
using Employees.Domain.ValueObjects;
using MediatR;

namespace Employees.App.ReadModel;

sealed class ReadModelHandlers :
    INotificationHandler<EmployeeCreatedEvent>
{
    private readonly IReadModelsContext _employeeRmsContext;

    public ReadModelHandlers(IReadModelsContext employeeRmsContext)
    {
        _employeeRmsContext = employeeRmsContext;
    }

    public Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
    {
        var employeeRm = new EmployeeRm
        {
            OrganizationId = notification.Employee.OrganizationId,
            Email = notification.Employee.Contacts?.FirstOrDefault(x => x.Type == ContactType.Email)?.Value,
            Id = notification.Employee.Id,
            Name = notification.Employee.FullName.Name,
            Patronymic = notification.Employee.FullName.Patronymic,
            Surname = notification.Employee.FullName.Surname,
            State = notification.Employee.State,
        };

        _employeeRmsContext.EmployeeRms.Add(employeeRm);

        return Task.CompletedTask;
    }
}
