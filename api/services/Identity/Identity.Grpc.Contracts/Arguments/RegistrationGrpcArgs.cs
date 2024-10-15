using ProtoBuf;
using System.Runtime.Serialization;

namespace Identity.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class RegistrationGrpcArgs
{

    private RegistrationGrpcArgs() { }

    public RegistrationGrpcArgs(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    [DataMember(Order = 1)]
    public string UserName { get; init; }

    [DataMember(Order = 2)]
    public string Password { get; init; }
}
