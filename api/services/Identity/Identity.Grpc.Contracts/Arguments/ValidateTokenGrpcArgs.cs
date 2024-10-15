using ProtoBuf;
using System.Runtime.Serialization;

namespace Identity.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class ValidateTokenGrpcArgs
{

    private ValidateTokenGrpcArgs() { }

    [DataMember(Order = 1)]
    public string AccessToken { get; init; }

    public ValidateTokenGrpcArgs(string accessToken)
    {
        AccessToken = accessToken;
    }
}
