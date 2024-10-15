using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Domain;

namespace Shared.Persistence.Converters;

public class StronglyTypedIdValueConverter<T>: ValueConverter<T, Guid>
    where T: StronglyTypedId, new()
{
    public StronglyTypedIdValueConverter(ConverterMappingHints? mappingHints = null) 
        : base(a => a.Value, id => StronglyTypedId.Create<T>(id), mappingHints)
    {
    }

}