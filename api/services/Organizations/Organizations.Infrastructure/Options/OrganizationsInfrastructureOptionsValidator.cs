using FluentValidation;

namespace Organizations.Infrastructure.Options;

sealed class OrganizationsInfrastructureOptionsValidator : AbstractValidator<OrganizationsInfrastructureOptions>
{

    public OrganizationsInfrastructureOptionsValidator()
    {
        RuleFor(x => x.DatabaseConnectionString).NotEmpty();
    }
}
