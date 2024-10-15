using Employees.Domain.Aggregates.Employees;
using Microsoft.EntityFrameworkCore;

namespace Employees.App.Infrastructure.DbContexts;

public interface IEmployeesContext
{
    DbSet<Employee> Employees { get; }
}
