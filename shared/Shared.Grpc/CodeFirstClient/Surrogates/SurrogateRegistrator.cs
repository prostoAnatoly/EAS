using ProtoBuf.Meta;

namespace Shared.Grpc.CodeFirstClient.Surrogates;

/// <summary>
/// Регистратор proto-контрактов.
/// </summary>
public static class SurrogateRegistrator
{
    /// <summary>
    /// Регистрирует все суррогаты proto-контрактов.
    /// </summary>
    public static void Register()
    {
        RuntimeTypeModel.Default.Add(typeof(DateTimeOffset), false).SetSurrogate(typeof(DateTimeOffsetSurrogate));
        RuntimeTypeModel.Default.Add(typeof(DateTimeOffset?), false).SetSurrogate(typeof(DateTimeOffsetSurrogate));
    }
}
