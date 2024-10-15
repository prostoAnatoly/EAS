using ProtoBuf;
using System.Runtime.Serialization;

namespace Identity.Grpc.Contracts.Responses;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class LoginGrpcResponse
{
    private LoginGrpcResponse() { }

    /// <summary>
    /// Токен доступа
    /// </summary>
    [DataMember(Order = 1)]
    public string AccessToken { get; private init; }

    /// <summary>
    /// Истекает, в секундах
    /// </summary>
    [DataMember(Order = 2)]
    public long ExpiresIn { get; private init; }

    /// <summary>
    /// Токена для обновления <see cref="AccessToken"/>
    /// </summary>л
    [DataMember(Order = 3)]
    public string RefreshToken { get; private init; }
}
