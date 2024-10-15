using FluentValidation;

namespace FilesStorage.App.Features;

sealed class DeleteValidatorValidator : AbstractValidator<DeleteCommand>
{

    public DeleteValidatorValidator()
    {
        RuleFor(x => x.FileId).NotEmpty().WithMessage("ИД файла пусто");
    }
}