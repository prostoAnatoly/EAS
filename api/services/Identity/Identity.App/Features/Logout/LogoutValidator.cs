using FluentValidation;

namespace Identity.App.Features.Logout;

sealed class LogoutValidator : AbstractValidator<LogoutCommand>
{

    public LogoutValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty().WithMessage("Не задан токен");
    }
}