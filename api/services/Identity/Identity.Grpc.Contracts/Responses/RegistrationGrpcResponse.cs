using ProtoBuf;
using System.Runtime.Serialization;

namespace Identity.Grpc.Contracts.Responses;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public class RegistrationGrpcResponse
{

    private RegistrationGrpcResponse() { }

    [DataMember(Order = 1)]
    public Guid IdentityId { get; private init; }
}
