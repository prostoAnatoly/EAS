using FluentValidation;
using SeedWork.Persistence;
using Shared.Infrastructure.PipelineInfrastructure;

namespace Employees.Infrastructure.Options;

public sealed class EmployeesInfrastructureOptions : IInfrastructureOptions, IDbContextOptions
{
    private readonly EmployeesInfrastructureOptionsValidator validator = new();

    public string DatabaseConnectionString { get; set; } = string.Empty;

    public void Validate()
    {
        validator.ValidateAndThrow(this);
    }
}
