using FluentValidation;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services.JwtSecurityToken;

class JwtSecurityTokenOptionsValidator : AbstractValidator<JwtSecurityTokenOptions>, IValidateOptions<JwtSecurityTokenOptions>
{
    public JwtSecurityTokenOptionsValidator()
    {
        RuleFor(x => x.Lifetime).GreaterThan(0);
        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.RefreshTokenExpiration).GreaterThan(0);
    }

    public ValidateOptionsResult Validate(string? name, JwtSecurityTokenOptions options)
    {
        var results = Validate(options);

        return results.IsValid
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(
                $"Ошибки конфигурации JWT-токена безопасности:{string.Join(' ', results.Errors.Select(r => r.ErrorMessage))}");
    }
}