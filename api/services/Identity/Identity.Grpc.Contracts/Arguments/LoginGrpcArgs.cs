using ProtoBuf;
using System.Runtime.Serialization;

namespace Identity.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class LoginGrpcArgs
{
    private LoginGrpcArgs() { }

    public LoginGrpcArgs(string userName, string password, string ipAddress, string userAgent)
    {
        UserName = userName;
        Password = password;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }

    [DataMember(Order = 1)]
    public string UserName { get; init; }

    [DataMember(Order = 2)]
    public string Password { get; init; }

    [DataMember(Order = 3)]
    public string IpAddress { get; init; }

    [DataMember(Order = 4)]
    public string UserAgent { get; init; }
}