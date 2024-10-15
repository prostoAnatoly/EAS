using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.App.Exceptions;

namespace Shared.Infrastructure.Behaviors.Validation;

public class ValidationBehavior<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    private readonly IServiceProvider _serviceProvider;
    private const string MESSAGE_ERROR_DEFAULT = "Неверный запрос";

    public ValidationBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        var validator = _serviceProvider.GetService<IValidator<TIn>>();
        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult?.IsValid == false)
            {
                var msg = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? MESSAGE_ERROR_DEFAULT;
                throw new BadRequestException(msg);
            }
        }

        return await next();
    }

}