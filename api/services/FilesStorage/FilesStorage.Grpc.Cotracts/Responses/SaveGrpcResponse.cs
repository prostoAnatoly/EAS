using ProtoBuf;
using System.Runtime.Serialization;

namespace FilesStorage.Grpc.Cotracts.Responses;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public class SaveGrpcResponse
{

    [DataMember(Order = 1)]
    public Guid FileId { get; init; }

}
