using Identity.Domain.Aggregates.AccessTokens;
using Identity.Domain.Aggregates.Identities;
using Identity.Domain.SizeFields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Persistence.Converters;

namespace Identity.Infrastructure.Persistence.Configurations;

sealed class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
{
    public void Configure(EntityTypeBuilder<AccessToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(new StronglyTypedIdValueConverter<AccessTokenId>())
            .ValueGeneratedNever();

        builder.Property(x => x.Value)
            .HasMaxLength(AccessTokenSizes.TOKEN_VALUE);

        builder.Property(x => x.IdentityId)
            .IsRequired()
            .HasConversion(new StronglyTypedIdValueConverter<IdentityId>());

        builder.HasOne<IdentityInfo>()
        .WithMany()
        .HasForeignKey(x => x.IdentityId);

        builder.OwnsOne(x => x.RefreshToken, x =>
            {
                x.WithOwner();

                x.Property(y => y.Fingerprint)
               .HasMaxLength(RefreshTokenSizes.FINGERPRINT);

                x.Property(y => y.Value)
                    .HasMaxLength(RefreshTokenSizes.TOKEN_VALUE);

                x.Property(y => y.CreatedByIp)
                    .HasMaxLength(RefreshTokenSizes.CREATED_BY_IP);
            }).Navigation(p => p.RefreshToken).IsRequired();
    }
}
