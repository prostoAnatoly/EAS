using Identity.Domain.Aggregates.Identities;
using Microsoft.EntityFrameworkCore;

namespace Identity.App.Infrastructure.DbContexts;

public interface IIdentitiesContext
{

    DbSet<IdentityInfo> Identities { get; }

}