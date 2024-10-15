using ProtoBuf;
using System.Runtime.Serialization;

namespace Identity.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class LogoutGrpcArgs
{

    private LogoutGrpcArgs() { }

    [DataMember(Order = 1)]
    public string? AccessToken { get; init; }

    public LogoutGrpcArgs(string? accessToken)
    {
        AccessToken = accessToken;
    }
}
