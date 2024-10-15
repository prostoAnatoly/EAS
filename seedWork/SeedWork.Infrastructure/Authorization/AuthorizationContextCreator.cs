using SeedWork.Application.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace SeedWork.Infrastructure.Authorization;

sealed class AuthorizationContextCreator : IAuthorizationContextCreator
{
    public IAuthorizationContext Create(JwtSecurityToken jwtSecurityToken)
    {
        throw new NotImplementedException();
    }
}
