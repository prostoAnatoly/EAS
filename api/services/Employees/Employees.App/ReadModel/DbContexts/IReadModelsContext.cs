using Employees.App.ReadModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.App.ReadModel.DbContexts;

public interface IReadModelsContext
{
    DbSet<EmployeeRm> EmployeeRms { get; }
}
