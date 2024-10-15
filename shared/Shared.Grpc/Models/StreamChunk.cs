using System.Runtime.Serialization;

namespace Shared.Grpc.Models;

/// <summary>
/// Контейнер для передачи данных частями.
/// </summary>
/// <typeparam name="T">Тип метаданных.</typeparam>
[DataContract]
public class StreamChunk<T>
    where T : class
{
    /// <summary>
    /// Часть данных.
    /// </summary>
    [DataMember(Order = 1)]
    public byte[] Chunk { get; init; }

    /// <summary>
    /// Метаданные.
    /// </summary>
    [DataMember(Order = 2)]
    public T? Meta { get; init; }
}
