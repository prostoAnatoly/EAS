using Identity.App.Infrastructure.DbContexts;
using Identity.Domain.Aggregates.Identities;
using Microsoft.EntityFrameworkCore;
using Shared.App.Exceptions;
using Shared.Mediator;

namespace Identity.App.Features.Registration;

sealed class RegistrationHandler : ICommandHandler<RegistrationCommand, RegistrationResult>
{
    private readonly IIdentitiesContext _identityContext;

    public RegistrationHandler(IIdentitiesContext identityContext)
    {
        _identityContext = identityContext;
    }

    public async Task<RegistrationResult> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        if (await _identityContext.Identities.AnyAsync(u => u.UserName == request.UserName, cancellationToken))
        {
            throw new BadRequestException("Пользователь уже зарегистрирован");
        }

        var identity = new IdentityInfo
        {
            UserName = request.UserName
        };

        identity.SetPassword(request.Password);

        _identityContext.Identities.Add(identity);

        return new RegistrationResult(identity.Id.Value);
    }
}
