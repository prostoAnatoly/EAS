using FluentValidation;

namespace Identity.App.Features.ValidateToken;

sealed class ValidateTokenValidatorValidator : AbstractValidator<ValidateTokenQuery>
{

    public ValidateTokenValidatorValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .WithMessage("Токен некорретный");
    }
}