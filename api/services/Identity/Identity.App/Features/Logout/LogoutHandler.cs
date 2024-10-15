using Identity.App.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Shared.App.Exceptions;
using Shared.Mediator;

namespace Identity.App.Features.Logout;

sealed class LogoutHandler : BaseCommandHandler<LogoutCommand>
{
    private readonly IAccessTokensContext accessTokenContext;

    public LogoutHandler(IAccessTokensContext accessTokenContext)
    {
        this.accessTokenContext = accessTokenContext;
    }

    protected override async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
       var accessToken = await accessTokenContext.AccessTokens.FirstOrDefaultAsync(x => x.Value == request.AccessToken,
           cancellationToken) ?? throw new BadRequestException("Пользователь не найден");

        accessTokenContext.AccessTokens.Remove(accessToken);
    }
}
