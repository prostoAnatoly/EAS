using AutoMapper;
using FilesStorage.App.Dtos;
using FilesStorage.App.Features;
using FilesStorage.Grpc.Cotracts;
using FilesStorage.Grpc.Cotracts.Arguments;
using FilesStorage.Grpc.Cotracts.Responses;
using MediatR;
using ProtoBuf.Grpc;
using SeedWork.Infrastructure.Helpers;
using Shared.Common.IO;
using Shared.Grpc.Extensions;
using Shared.Grpc.Models;

namespace FilesStorage.Grpc.Services
{
    public class FilesStorageServiceGrpc : IFilesStorageServiceGrpc
    {
        private readonly MediatorHelper mediatorHelper;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public FilesStorageServiceGrpc(MediatorHelper mediatorHelper, IMapper mapper, IMediator mediator)
        {
            this.mediatorHelper = mediatorHelper;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task Delete(DeleteGrpcArgs args, CallContext context = default)
        {
            await mediatorHelper.Command<DeleteCommand>(args);
        }

        public async IAsyncEnumerable<StreamChunk<DownloadGrpcResponse>> Download(DownloadGrpcArgs args, CallContext context = default)
        {
            var (result, attrs) = await mediatorHelper.QueryWithIntermediate<DownloadQuery, DownloadResult, DownloadGrpcResponse>(args, context.CancellationToken);

            await foreach (var chunk in result.FileStream.ToGrpcChankedStream(attrs))
            {
                yield return chunk;
            }
        }

        public async Task<SaveGrpcResponse> Save(IAsyncEnumerable<StreamChunk<SaveGrpcArgs>> args, CallContext context = default)
        {
            var asyncFile = await args.ReadGrpcChunkedStream(context.CancellationToken);

            var command = new SaveCommand
            {
                File = mapper.Map<AsyncFile<FilePropsDto>>(asyncFile),
            };

            var fileId = await mediator.Send(command);

            return new SaveGrpcResponse
            {
                FileId = fileId,
            };
        }
    }
}