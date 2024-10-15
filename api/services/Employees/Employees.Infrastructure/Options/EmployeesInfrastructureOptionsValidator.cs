using FluentValidation;

namespace Employees.Infrastructure.Options;

sealed class EmployeesInfrastructureOptionsValidator : AbstractValidator<EmployeesInfrastructureOptions>
{

    public EmployeesInfrastructureOptionsValidator()
    {
        RuleFor(x => x.DatabaseConnectionString).NotEmpty();
    }
}
