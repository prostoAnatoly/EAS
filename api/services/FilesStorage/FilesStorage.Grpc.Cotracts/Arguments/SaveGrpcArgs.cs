using ProtoBuf;
using System.Runtime.Serialization;

namespace FilesStorage.Grpc.Cotracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class SaveGrpcArgs
{

    [DataMember(Order = 1)]
    public string FileName { get; init; }
}