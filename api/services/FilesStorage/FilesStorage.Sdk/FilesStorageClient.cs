using FilesStorage.Grpc.Cotracts;
using FilesStorage.Grpc.Cotracts.Arguments;
using FilesStorage.Grpc.Cotracts.Responses;
using SeedWork.Infrastructure.Extensions;
using Shared.Grpc.Models;

namespace FilesStorage.Sdk;

public class FilesStorageClient
{

    private readonly IFilesStorageServiceGrpc filesStorageServiceGrpc;

    public FilesStorageClient(IFilesStorageServiceGrpc filesStorageServiceGrpc)
    {
        this.filesStorageServiceGrpc = filesStorageServiceGrpc;
    }

    public async IAsyncEnumerable<StreamChunk<DownloadGrpcResponse>> Download(DownloadGrpcArgs args)
    {
        await foreach(var chunck in filesStorageServiceGrpc.Download(args).TryCatchGrpcToAppException())
        {
            yield return chunck;
        }
    }

    public async Task<SaveGrpcResponse> Save(IAsyncEnumerable<StreamChunk<SaveGrpcArgs>> args)
    {
        return await filesStorageServiceGrpc.Save(args).TryCatchGrpcToAppException();
    }

    public async Task Delete(DeleteGrpcArgs args)
    {
        await filesStorageServiceGrpc.Delete(args).TryCatchGrpcToAppException();
    }
}