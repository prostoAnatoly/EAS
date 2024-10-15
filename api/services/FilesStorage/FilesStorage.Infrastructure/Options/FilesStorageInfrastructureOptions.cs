using FluentValidation;
using SeedWork.Persistence;
using Shared.Infrastructure.PipelineInfrastructure;

namespace FilesStorage.Infrastructure.Options;

public sealed class FilesStorageInfrastructureOptions : IInfrastructureOptions, IDbContextOptions
{

    private readonly FilesStorageInfrastructureOptionsValidator validator = new();

    public string DatabaseConnectionString { get; set; } = string.Empty;

    public void Validate()
    {
        validator.ValidateAndThrow(this);
    }

}
