using SeedWork.Application.Authorization;

namespace SeedWork.Infrastructure.Authorization;

public interface IAuthorizationService
{

    IAuthorizationContext GetAuthorizationContext();
}
