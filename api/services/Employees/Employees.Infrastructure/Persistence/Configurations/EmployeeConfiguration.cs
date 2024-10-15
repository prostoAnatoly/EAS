using Employees.Domain.Aggregates.Employees;
using Employees.Domain.SizeFields;
using Employees.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Common.Extensions;
using Shared.Persistence.Converters;

namespace Employees.Infrastructure.Persistence.Configurations;

sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    private static readonly string _nameId = "Id".ToSnakeCase();

    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(new StronglyTypedIdValueConverter<EmployeeId>())
            .ValueGeneratedNever();

        builder.Property(x => x.State)
            .HasConversion(new EnumToStringConverter<EmployeeState>());

        builder.OwnsMany(x => x.Contacts, navigationBuilder =>
            {
                
                navigationBuilder.Property<Guid>(_nameId);
                navigationBuilder.HasKey(_nameId);

                var fkName = nameof(EmployeeId).ToSnakeCase();
                navigationBuilder
                    .WithOwner()
                    .HasForeignKey(fkName);

                navigationBuilder
                    .Property<EmployeeId>(fkName)
                    .HasConversion(new StronglyTypedIdValueConverter<EmployeeId>());

                navigationBuilder.Property(a => a.Type)
                    .HasConversion(new EnumToStringConverter<ContactType>());

                navigationBuilder.Property(x => x.Value)
                    .HasMaxLength(EmployeeSizes.CONTACT_VALUE);

            });

        builder.Property(x => x.OrganizationId)
            .HasConversion(new StronglyTypedIdValueConverter<OrganizationId>())
            .ValueGeneratedNever();

        builder.OwnsOne(x => x.FullName, x =>
            {
                x.WithOwner();

                x.Property(y => y.Name)
                    .HasMaxLength(FullNameSizes.NAME);

                x.Property(y => y.Surname)
                    .HasMaxLength(FullNameSizes.SURNAME);

                x.Property(y => y.Patronymic)
                    .HasMaxLength(FullNameSizes.PATRONYMIC);

            })
            .Navigation(p => p.FullName)
            .IsRequired();
    }
}