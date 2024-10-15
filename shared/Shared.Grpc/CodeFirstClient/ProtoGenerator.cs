using ProtoBuf.Grpc.Reflection;
using ProtoBuf.Meta;
using System.Reflection;
using System.ServiceModel;

namespace Shared.Grpc.CodeFirstClient;

public static class ProtoGenerator
{
    /// <summary>
    /// Генерация proto файла для клиентов.
    /// </summary>
    /// <typeparam name="T">Интерфейс описывающий контракты.</typeparam>
    /// <remarks>
    /// Генерация должна происходить после конфигурирования StartUp.
    /// </remarks>
    public static async Task GenerateProto<T>() where T : class
    {
        await GenerateProto(typeof(T));
    }

    /// <summary>
    /// Генерация proto файлой для клиентов.
    /// </summary>
    /// <param name="contractAssembly">Сборка содержащая интерфейсы описывающие контракты.</param>
    /// <remarks>
    /// Генерация должна происходить после конфигурирования StartUp.
    /// </remarks>
    public static async Task GenerateProtos(Assembly contractAssembly)
    {
        if (contractAssembly.IsDynamic) { return; }

        foreach (var type in contractAssembly.GetExportedTypes())
        {
            if (type.IsInterface && type.GetCustomAttribute<ServiceContractAttribute>() != null)
            {
                await GenerateProto(type);
            }
        }
    }

    private static async Task GenerateProto(Type contractType)
    {
        var generator = new SchemaGenerator
        {
            ProtoSyntax = ProtoSyntax.Proto3
        };

        var schema = generator.GetSchema(contractType);

        await using var writer = new StreamWriter($"{contractType.Name}.proto");
        await writer.WriteAsync(schema);
    }
}
