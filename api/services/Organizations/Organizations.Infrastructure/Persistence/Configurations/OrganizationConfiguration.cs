using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organizations.Domain.Aggregates.Organizations;
using Organizations.Domain.SizeFields;
using Organizations.Domain.ValueObjects;
using Shared.Persistence.Converters;

namespace Organizations.Infrastructure.Persistence.Configurations;

sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.OwnerId);

        builder.Property(x => x.Id)
            .HasConversion(new StronglyTypedIdValueConverter<OrganizationId>())
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(OrganizationSizes.NAME);

        builder.Property(x => x.CreatorId)
            .HasConversion(new StronglyTypedIdValueConverter<UserId>())
            .ValueGeneratedNever();

        builder.Property(x => x.OwnerId)
            .HasConversion(new StronglyTypedIdValueConverter<UserId>())
            .ValueGeneratedNever();

        builder.OwnsOne(x => x.SearchInfo, x =>
            {
                x.WithOwner();

                x.Property(x => x.Name);
            })
            .Navigation(x => x.SearchInfo)
            .IsRequired();
    }
}