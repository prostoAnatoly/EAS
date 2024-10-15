using System.Security.Claims;

namespace Shared.Rest.Common.JwtToken;

public interface IIdentityEnricher
{

    void Enrich(ClaimsIdentity? identity, Guid? identityId);
}
