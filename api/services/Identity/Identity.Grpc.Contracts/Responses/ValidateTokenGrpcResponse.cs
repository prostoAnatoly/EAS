using ProtoBuf;
using System.Runtime.Serialization;

namespace Identity.Grpc.Contracts.Responses;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public class ValidateTokenGrpcResponse
{

    private ValidateTokenGrpcResponse() { }

    /// <summary>
    /// Токен доступа
    /// </summary>
    [DataMember(Order = 1)]
    public bool IsValid { get; private init; }

    [DataMember(Order = 2)]
    public Guid IdentityId { get; private init; }
}
