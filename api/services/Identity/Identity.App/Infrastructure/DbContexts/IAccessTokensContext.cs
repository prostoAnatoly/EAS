using Identity.Domain.Aggregates.AccessTokens;
using Microsoft.EntityFrameworkCore;

namespace Identity.App.Infrastructure.DbContexts;

public interface IAccessTokensContext
{

    DbSet<AccessToken> AccessTokens { get; }

}