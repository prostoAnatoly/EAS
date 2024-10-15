using Shared.Mediator.Generics;

namespace Identity.App.Features.Login;

public class LoginCommand : ICommand<LoginResult>
{

    public string UserName { get; init; }

    public string Password { get; init; }

    public string IpAddress { get; init; }

    public string UserAgent { get; init; }

    private LoginCommand() { }

    public LoginCommand(string userName, string password, string ipAddress, string userAgent)
    {
        UserName = userName;
        Password = password;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
}