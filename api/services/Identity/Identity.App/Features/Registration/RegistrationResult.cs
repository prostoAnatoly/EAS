namespace Identity.App.Features.Registration;

public class RegistrationResult
{

    public Guid IdentityId { get; }


    public RegistrationResult(Guid identityId)
    {
        IdentityId = identityId;
    }
}