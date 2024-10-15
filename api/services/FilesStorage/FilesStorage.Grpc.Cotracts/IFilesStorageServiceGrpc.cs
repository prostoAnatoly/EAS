using FilesStorage.Grpc.Cotracts.Arguments;
using FilesStorage.Grpc.Cotracts.Responses;
using ProtoBuf.Grpc;
using Shared.Grpc.Models;
using System.ServiceModel;

namespace FilesStorage.Grpc.Cotracts;

[ServiceContract]
public interface IFilesStorageServiceGrpc
{

    [OperationContract]
    IAsyncEnumerable<StreamChunk<DownloadGrpcResponse>> Download(DownloadGrpcArgs args, CallContext context = default);

    [OperationContract]
    Task<SaveGrpcResponse> Save(IAsyncEnumerable<StreamChunk<SaveGrpcArgs>> args, CallContext context = default);

    [OperationContract]
    Task Delete(DeleteGrpcArgs args, CallContext context = default);
}