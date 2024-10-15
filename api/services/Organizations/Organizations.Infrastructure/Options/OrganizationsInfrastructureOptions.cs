using FluentValidation;
using SeedWork.Persistence;
using Shared.Infrastructure.PipelineInfrastructure;

namespace Organizations.Infrastructure.Options;

public sealed class OrganizationsInfrastructureOptions : IInfrastructureOptions, IDbContextOptions
{
    private readonly OrganizationsInfrastructureOptionsValidator validator = new();

    public string DatabaseConnectionString { get; set; } = string.Empty;

    public void Validate()
    {
        validator.ValidateAndThrow(this);
    }
}
