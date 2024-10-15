using Employees.App.ReadModel.Models;
using Employees.Domain.Aggregates.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Employees.Infrastructure.Persistence.Configurations;

sealed class ReadModelsConfiguration : IEntityTypeConfiguration<EmployeeRm>
{
    public void Configure(EntityTypeBuilder<EmployeeRm> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.State)
            .HasConversion(new EnumToStringConverter<EmployeeState>());
    }
}