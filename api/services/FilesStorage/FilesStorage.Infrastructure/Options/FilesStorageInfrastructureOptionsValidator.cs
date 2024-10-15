using FluentValidation;

namespace FilesStorage.Infrastructure.Options;

public sealed class FilesStorageInfrastructureOptionsValidator : AbstractValidator<FilesStorageInfrastructureOptions>
{
    public FilesStorageInfrastructureOptionsValidator()
    {
        RuleFor(x => x.DatabaseConnectionString).NotEmpty();
    }
}
