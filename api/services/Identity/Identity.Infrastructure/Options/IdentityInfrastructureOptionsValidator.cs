using FluentValidation;

namespace Identity.Infrastructure.Options;

public sealed class IdentityInfrastructureOptionsValidator : AbstractValidator<IdentityInfrastructureOptions>
{
    public IdentityInfrastructureOptionsValidator()
    {
        RuleFor(x => x.DatabaseConnectionString).NotEmpty();
    }
}
