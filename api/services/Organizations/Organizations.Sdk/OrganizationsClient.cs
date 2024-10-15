using Grpc.Core;
using Organizations.Grpc.Contracts;
using Organizations.Grpc.Contracts.Arguments;
using Organizations.Grpc.Contracts.Responses;
using SeedWork.Infrastructure.Extensions;
using Shared.Domain.Exceptions;

namespace Organizations.Sdk;

public sealed class OrganizationsClient
{
    private readonly IOrganizationsServiceGrpc _organizationsServiceGrpc;

    public OrganizationsClient(IOrganizationsServiceGrpc organizationsServiceGrpc)
    {
        _organizationsServiceGrpc = organizationsServiceGrpc;
    }

    public async Task<CreateOrganizationGrpcResponse> CreateOrganization(CreateOrganizationArgs args)
    {
        return await _organizationsServiceGrpc.CreateOrganization(args).TryCatchGrpcToAppException(MapException);

        static Exception? MapException(RpcException rpcEx) => rpcEx.StatusCode switch
        {
            StatusCode.AlreadyExists => new DbForeignKeyException("Такой пользователь уже существует"),
            _ => null,
        };
    }

    public async Task<GetOrganizationsGrpcResponse> GetOrganizations(GetOrganizationsArgs args)
    {
        return await _organizationsServiceGrpc.GetOrganizations(args).TryCatchGrpcToAppException();
    }
}