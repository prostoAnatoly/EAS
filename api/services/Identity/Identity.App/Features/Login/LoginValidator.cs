using FluentValidation;
using Shared.Common.Extensions;

namespace Identity.App.Features.Login;

sealed class LoginValidator : AbstractValidator<LoginCommand>
{

    public LoginValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Не задано имя");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Не задан пароль")
            .Must(x => x.IsBase64()).WithMessage("Неверно имя или пароль");
    }
}