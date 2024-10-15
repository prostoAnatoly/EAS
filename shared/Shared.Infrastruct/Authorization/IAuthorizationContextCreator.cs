using Shared.App.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace Shared.Infrastruct.Authorization;

public interface IAuthorizationContextCreator
{

    IAuthorizationContext Create(JwtSecurityToken jwtSecurityToken);
}
