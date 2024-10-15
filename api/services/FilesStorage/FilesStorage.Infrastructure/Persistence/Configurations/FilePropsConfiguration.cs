using FilesStorage.Domain.Aggregates.Files;
using Identity.Domain.SizeFields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Persistence.Converters;

namespace FilesStorage.Infrastructure.Persistence.Configurations;

public class FilePropsConfiguration: IEntityTypeConfiguration<FileProps>
{
    public void Configure(EntityTypeBuilder<FileProps> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(new StronglyTypedIdValueConverter<FileId>())
            .ValueGeneratedNever();

        builder.Property(x => x.FileName)
            .HasMaxLength(FilePropsSizes.FILE_NAME);
    }
}