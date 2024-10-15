using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Shared.Grpc.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Shared.Infrastruct.Behaviors.Authorization;

sealed class AuthorizationBehavior<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    private readonly IServiceProvider _serviceProvider;

    public AuthorizationBehavior(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        var token = GetToken();

        return await next();
    }

    private HttpContext? GetHttpContext()
    {
        return _serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext // HTTP 1.0 API
                              ?? _serviceProvider.GetService<GrpcContext>()?.HttpContext; // HTTP 2.0 GRPC
    }

    private JwtSecurityToken? GetToken()
    {
        var httpContext = GetHttpContext();
        if (httpContext == null) { return null; }

        if (httpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authHeader) &&
            AuthenticationHeaderValue.TryParse(authHeader, out var headerInfo) &&
            headerInfo.Scheme == JwtBearerDefaults.AuthenticationScheme &&
            !string.IsNullOrEmpty(headerInfo.Parameter))
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(headerInfo.Parameter);
        }

        return null;
    }
}
