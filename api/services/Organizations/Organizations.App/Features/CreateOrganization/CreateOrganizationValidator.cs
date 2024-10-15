using FluentValidation;
using Organizations.Domain.SizeFields;

namespace Organizations.App.Features;

sealed class CreateOrganizationValidatorValidator : AbstractValidator<CreateOrganizationCommand>
{

    public CreateOrganizationValidatorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Не задано наименование организации")
            .MaximumLength(OrganizationSizes.NAME)
            .WithMessage($"Наименование организации не может быть длиннее чем {OrganizationSizes.NAME}");

        RuleFor(x => x.CreatorId)
            .NotEmpty()
            .WithMessage("Не задан автор организации");
    }
}