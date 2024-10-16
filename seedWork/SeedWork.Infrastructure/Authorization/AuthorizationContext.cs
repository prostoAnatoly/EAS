using SeedWork.Application.Authorization;

namespace SeedWork.Infrastructure.Authorization;

public sealed class AuthorizationContext : IAuthorizationContext
{
    public Guid UserId { get; internal set; }
}
