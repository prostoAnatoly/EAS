using Shared.Mediator;

namespace Identity.App.Features.ValidateToken;

public class ValidateTokenQuery : IQuery<ValidateTokenResult>
{

    public string AccessToken { get; }

    public ValidateTokenQuery(string accessToken)
    {
        AccessToken = accessToken;
    }
}