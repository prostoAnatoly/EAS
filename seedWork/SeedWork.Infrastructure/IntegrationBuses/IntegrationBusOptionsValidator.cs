using FluentValidation;
using Microsoft.Extensions.Options;

namespace SeedWork.Infrastructure.IntegrationBuses;

class IntegrationBusOptionsValidator : AbstractValidator<IntegrationBusOptions>, IValidateOptions<IntegrationBusOptions>
{
    public IntegrationBusOptionsValidator()
    {
        When(x => x.Enabled, () =>
        {
            RuleFor(x => x.Host).NotEmpty();
            RuleFor(x => x.VirtualHost).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ReceiveEndpoint).NotEmpty();
            RuleFor(x => x.RetryCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.RetryInitialIntervalMs).GreaterThanOrEqualTo(0);
            RuleFor(x => x.RetryIntervalIncrementMs).GreaterThanOrEqualTo(0);
        });
    }

    public ValidateOptionsResult Validate(string? name, IntegrationBusOptions options)
    {
        var results = Validate(options);

        return results.IsValid
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(
                $"Ошибки конфигурации интеграционной шины:{string.Join(' ', results.Errors.Select(r => r.ErrorMessage))}");
    }
}