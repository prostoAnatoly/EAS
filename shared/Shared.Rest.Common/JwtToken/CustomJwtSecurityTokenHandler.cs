using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shared.Rest.Common.JwtToken;

/// <summary>
/// Переопределения обработчика безопасности JWT
/// </summary>
/// <remarks>
/// Инициализирует новый экземпляр класса <see cref="CustomJwtSecurityTokenHandler"/>
/// </remarks>
/// <param name="logger"></param>
public class CustomJwtSecurityTokenHandler: ISecurityTokenValidator
{
    private readonly ILogger<CustomJwtSecurityTokenHandler> _logger;
    private readonly IExternalJwtSecurityTokenValidator? _externalJwtSecurityTokenValidator;
    private readonly IIdentityEnricher? _identityEnricher;

    private int maxTokenSizeInBytes = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;
    private readonly JwtSecurityTokenHandler tokenHandler = new();

    public CustomJwtSecurityTokenHandler(ILogger<CustomJwtSecurityTokenHandler> logger,
        IExternalJwtSecurityTokenValidator? externalJwtSecurityTokenValidator,
        IIdentityEnricher? identityEnricher)
    {
        _logger = logger;
        _externalJwtSecurityTokenValidator = externalJwtSecurityTokenValidator;
        _identityEnricher = identityEnricher;
    }

    /// <inheritdoc />
    public bool CanValidateToken { get { return true; } }

    /// <inheritdoc />
    public int MaximumTokenSizeInBytes
    {
        get
        {
            return maxTokenSizeInBytes;
        }
        set
        {
            maxTokenSizeInBytes = value;
        }
    }

    /// <inheritdoc />
    public bool CanReadToken(string securityToken)
    {
        return tokenHandler.CanReadToken(securityToken);
    }

    /// <inheritdoc />
    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        try
        {
            var principal = tokenHandler.ValidateToken(securityToken, validationParameters, out validatedToken);
            var identity = principal.Identity as ClaimsIdentity;

            ExternalValidateClaimsIdentity(identity?.Clone());

            var validationResult = ExternalValidateToken(securityToken)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            _identityEnricher?.Enrich(identity, validationResult?.IdentityId);

            return principal;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Невозможно проверить пришедший Token");

            throw;
        }
    }

    private async Task<ValidationResult?> ExternalValidateToken(string securityToken)
    {
        if (_externalJwtSecurityTokenValidator == null) { return null; }

        var validationResult = await _externalJwtSecurityTokenValidator.ValidateToken(securityToken);

        if (validationResult.IsValid == false)
        {
            throw new InvalidTokenException("Токен недействительный");
        }

        return validationResult;
    }

    private void ExternalValidateClaimsIdentity(ClaimsIdentity? identity)
    {
        if (_externalJwtSecurityTokenValidator == null) { return; }

        _externalJwtSecurityTokenValidator.ValidateClaimsIdentityThrow(identity);

    }
}