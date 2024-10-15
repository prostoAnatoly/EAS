namespace Shared.Rest.Common.JwtToken;

public record ValidationResult(bool IsValid, Guid IdentityId);
