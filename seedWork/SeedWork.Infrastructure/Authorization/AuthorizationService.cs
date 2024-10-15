using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using SeedWork.Application.Authorization;
using Shared.Grpc.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace SeedWork.Infrastructure.Authorization;

sealed class AuthorizationService : IAuthorizationService
{
    private readonly HttpContext? _httpContext;
    private readonly IAuthorizationContextCreator _authorizationContextCreator;

    private IAuthorizationContext? _authorizationContext;

    protected AuthorizationService(IHttpContextAccessor? httpContextAccessor, GrpcContext? grpcContext,
        IAuthorizationContextCreator authorizationContextCreator)
    {
        _httpContext = httpContextAccessor?.HttpContext ?? grpcContext?.HttpContext;
        _authorizationContextCreator = authorizationContextCreator;
    }

    public IAuthorizationContext? GetAuthorizationContext()
    {
        if (_authorizationContext == null)
        {
            var token = GetToken();
            if (token != null)
            {
                _authorizationContext = _authorizationContextCreator.Create(token);
            }
        }

        return _authorizationContext;
    }

    private JwtSecurityToken? GetToken()
    {
        if (_httpContext == null) { return null; }

        if (_httpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authHeader) &&
            AuthenticationHeaderValue.TryParse(authHeader, out var headerInfo) &&
            headerInfo.Scheme == JwtBearerDefaults.AuthenticationScheme &&
            !string.IsNullOrEmpty(headerInfo.Parameter))
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(headerInfo.Parameter);
        }

        return null;
    }
}
