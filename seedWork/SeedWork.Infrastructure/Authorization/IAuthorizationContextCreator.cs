using SeedWork.Application.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace SeedWork.Infrastructure.Authorization;

public interface IAuthorizationContextCreator
{

    IAuthorizationContext Create(JwtSecurityToken jwtSecurityToken);
}
