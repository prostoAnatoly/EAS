using Eas.Gate.Ui.JwtToken;
using EasGateUi.JwtToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Rest.Common.JwtToken;
using System.IdentityModel.Tokens.Jwt;

namespace EasGateUi;

static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationBearer(this IServiceCollection services)
    {
        services.AddSingleton<ISecurityTokenValidator, CustomJwtSecurityTokenHandler>();
        services.AddSingleton<IExternalJwtSecurityTokenValidator, ExternalJwtSecurityTokenValidator>();
        services.AddSingleton<IIdentityEnricher, IdentityEnricher>();

        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                            .Configure<ISecurityTokenValidator>((options, validator) =>
                            {
                                options.SaveToken = false;
                                options.SecurityTokenValidators.Clear();
                                options.SecurityTokenValidators.Add(validator);

                                options.UseSecurityTokenValidators = true;

                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateLifetime = false,
                                    ValidateIssuerSigningKey = false,
                                    ValidateIssuer = false,
                                    ClockSkew = TimeSpan.Zero,
                                    ValidateAudience = false,
                                    TryAllIssuerSigningKeys = false,
                                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                                    {
                                        return new JwtSecurityToken(token);
                                    },
                                };
                            });
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();

        return services;
    }

}
