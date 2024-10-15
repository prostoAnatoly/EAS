using System.Runtime.Serialization;

namespace Shared.Grpc.CodeFirstClient.Surrogates;

/// <summary>
/// Суррогат для обёртки типа данных <see cref="DateTimeOffset"/>
/// </summary>
[DataContract(Name = nameof(DateTimeOffset))]
public class DateTimeOffsetSurrogate
{
    [DataMember(Order = 1)]
    public DateTime? UtcTime { get; set; }

    [DataMember(Order = 2)]
    public TimeSpan? Offset { get; set; }

    /// <summary>
    /// Десериализует в <see cref="DateTimeOffset"/>
    /// </summary>
    public static implicit operator DateTimeOffset(DateTimeOffsetSurrogate surrogate)
    {
        var result = new DateTimeOffset(surrogate.UtcTime.Value);
        
        return result.ToOffset(surrogate.Offset.Value);
    }

    /// <summary>
    /// Десериализует в nullable <see cref="DateTimeOffset"/>
    /// </summary>
    public static implicit operator DateTimeOffset?(DateTimeOffsetSurrogate surrogate)
    {
        if (surrogate.Offset.HasValue && surrogate.UtcTime.HasValue)
        {
            var result = new DateTimeOffset(surrogate.UtcTime.Value);
            return result.ToOffset(surrogate.Offset.Value);
        }

        return default;
    }

    /// <summary>
    /// Сериализует в <see cref="DateTimeOffsetSurrogate"/>
    /// </summary>
    public static implicit operator DateTimeOffsetSurrogate(DateTimeOffset source)
    {
        return new()
        {
            UtcTime = source.UtcDateTime,
            Offset = source.Offset
        };
    }

    /// <summary>
    /// Десериализует в nullable <see cref="DateTimeOffsetSurrogate"/>
    /// </summary>
    public static implicit operator DateTimeOffsetSurrogate(DateTimeOffset? source)
    {
        return new()
        {
            UtcTime = source?.UtcDateTime,
            Offset = source?.Offset
        };
    }

}
