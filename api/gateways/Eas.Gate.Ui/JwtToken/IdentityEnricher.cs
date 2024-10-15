using Shared.Rest.Common.JwtToken;
using System.Security.Claims;

namespace Eas.Gate.Ui.JwtToken;

public class IdentityEnricher : IIdentityEnricher
{
    public static string ClaimNameUserId { get; } = "userId";

    public void Enrich(ClaimsIdentity? identity, Guid? identityId)
    {
        if (identity == null) { return; }

        if (identityId.HasValue)
        {
            var claim = new Claim(ClaimNameUserId, identityId.Value.ToString());

            identity.AddClaim(claim);
        }
    }
}