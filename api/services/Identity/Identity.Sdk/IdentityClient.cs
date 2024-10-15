using Grpc.Core;
using Identity.App.Infrastructure.Services;
using Identity.Grpc.Contracts;
using Identity.Grpc.Contracts.Arguments;
using Identity.Grpc.Contracts.Responses;
using SeedWork.Infrastructure.Extensions;
using Shared.Domain.Exceptions;

namespace Identity.Sdk;

public class IdentityClient
{
    private readonly IIdentityServiceGrpc _identityServiceGrpc;

    public IdentityClient(IIdentityServiceGrpc identityServiceGrpc)
    {
        _identityServiceGrpc = identityServiceGrpc;
    }

    public async Task<LoginGrpcResponse> Login(LoginGrpcArgs args)
    {
        return await _identityServiceGrpc.Login(args).TryCatchGrpcToAppException();
    }

    public async Task Logout(LogoutGrpcArgs args)
    {
        await _identityServiceGrpc.Logout(args).TryCatchGrpcToAppException();
    }

    public async Task<RegistrationGrpcResponse> Registration(RegistrationGrpcArgs args)
    {
        return await _identityServiceGrpc.Registration(args).TryCatchGrpcToAppException(MapException);

        static Exception? MapException(RpcException rpcEx) => rpcEx.StatusCode switch
        {
            StatusCode.AlreadyExists => new DbForeignKeyException("Такой пользователь уже существует"),
            _ => null,
        };
    }

    public async Task<ValidateTokenGrpcResponse> ValidateToken(ValidateTokenGrpcArgs args)
    {
        return await _identityServiceGrpc.ValidateToken(args).TryCatchGrpcToAppException();
    }
}