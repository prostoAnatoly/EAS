using Identity.Domain.Aggregates.Identities;
using Identity.Domain.SizeFields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Persistence.Converters;

namespace Identity.Infrastructure.Persistence.Configurations;

sealed class IdentityConfiguration : IEntityTypeConfiguration<IdentityInfo>
{
    public void Configure(EntityTypeBuilder<IdentityInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.UserName).IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(new StronglyTypedIdValueConverter<IdentityId>())
            .ValueGeneratedNever();

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(IdentityInfoSizes.USER_NAME);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(IdentityInfoSizes.PASSWORD);
    }
}
