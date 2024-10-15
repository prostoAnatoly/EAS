using FluentValidation;
using SeedWork.Persistence;
using Shared.Infrastructure.PipelineInfrastructure;

namespace Identity.Infrastructure.Options;

public sealed class IdentityInfrastructureOptions : IInfrastructureOptions, IDbContextOptions
{

    private readonly IdentityInfrastructureOptionsValidator validator = new();

    public string DatabaseConnectionString { get; set; } = string.Empty;

    public void Validate()
    {
        validator.ValidateAndThrow(this);
    }

}
