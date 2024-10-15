using ProtoBuf;
using System.Runtime.Serialization;

namespace FilesStorage.Grpc.Cotracts.Arguments
{
    [DataContract]
    [CompatibilityLevel(CompatibilityLevel.Level300)]
    public sealed class DeleteGrpcArgs
    {
        [DataMember(Order = 1)]
        public Guid FileId { get; init; }
    }
}