using FluentValidation;

namespace FilesStorage.App.Features;

sealed class DownloadValidatorValidator : AbstractValidator<DownloadQuery>
{

    public DownloadValidatorValidator()
    {
        RuleFor(x => x.FileId).NotEmpty().WithMessage("ИД файла пусто");
    }
}