using AutoMapper;
using FilesStorage.App.Dtos;
using FilesStorage.App.Features;
using FilesStorage.Grpc.Cotracts.Arguments;
using FilesStorage.Grpc.Cotracts.Responses;
using Shared.Common.IO;

namespace FilesStorage.Grpc.MappingProfiles;

public class FilesStorageServiceMappingProfile : Profile
{

    public FilesStorageServiceMappingProfile()
    {
        MapLogin();
    }

    private void MapLogin()
    {
        CreateMap<DownloadGrpcArgs, DownloadQuery>();
        CreateMap<DeleteGrpcArgs, DeleteCommand>();
        CreateMap<SaveGrpcArgs, FilePropsDto>();
        CreateMap<AsyncFile<SaveGrpcArgs>, AsyncFile<FilePropsDto>>();

        CreateMap<DownloadResult, DownloadGrpcResponse>();
    }
}