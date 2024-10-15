using Shared.Mediator.Generics;

namespace Identity.App.Features.Registration;

public class RegistrationCommand : ICommand<RegistrationResult>
{

    public string UserName { get; init; }

    public string Password { get; init; }

    private RegistrationCommand() { }

    public RegistrationCommand(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}