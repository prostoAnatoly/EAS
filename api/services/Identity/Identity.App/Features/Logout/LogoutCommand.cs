using Shared.Mediator;

namespace Identity.App.Features.Logout;

public class LogoutCommand : ICommand
{

    public string AccessToken { get; init; }

    private LogoutCommand() { }

    public LogoutCommand(string accessToken)
    {
        AccessToken = accessToken;
    }
}