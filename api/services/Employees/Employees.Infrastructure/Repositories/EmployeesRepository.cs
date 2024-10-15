using Employees.App.Infrastructure.DbContexts;
using Employees.Domain.Aggregates.Employees;

namespace Employees.Infrastructure.Repositories;

sealed class EmployeesRepository : IEmployeesRepository
{
    private readonly IEmployeesContext _employeesContext;

    public EmployeesRepository(IEmployeesContext employeesContext)
    {
        _employeesContext = employeesContext;
    }

    public void AddEmployee(Employee employee)
    {
        _employeesContext.Employees.Add(employee);
    }
}
