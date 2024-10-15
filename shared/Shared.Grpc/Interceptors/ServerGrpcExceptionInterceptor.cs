using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Shared.Grpc.Interceptors;

public class ServerGrpcExceptionInterceptor : Interceptor
{
    private readonly ILogger<ServerGrpcExceptionInterceptor> logger;
    private readonly IGrpcStatusCodeDefiner statusCodeDefiner;

    public ServerGrpcExceptionInterceptor(ILogger<ServerGrpcExceptionInterceptor> logger, IGrpcStatusCodeDefiner statusCodeDefiner)
    {
        this.logger = logger;
        this.statusCodeDefiner = statusCodeDefiner;
    }

    /// <inheritdoc/>
    public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context,
        DuplexStreamingServerMethod<TRequest, TResponse> continuation
    )
    {
        try
        {
            await base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
        }
        catch (Exception baseException)
        {
            throw DefineException(baseException);
        }
    }

    /// <inheritdoc/>
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation
    )
    {
        try
        {
            return await base.UnaryServerHandler(request, context, continuation);
        }
        catch (Exception baseException)
        {
            throw DefineException(baseException);
        }
    }

    /// <inheritdoc/>
    public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
        IAsyncStreamReader<TRequest> requestStream,
        ServerCallContext context,
        ClientStreamingServerMethod<TRequest, TResponse> continuation
    )
    {
        try
        {
            return await base.ClientStreamingServerHandler(requestStream, context, continuation);
        }
        catch (Exception baseException)
        {
            throw DefineException(baseException);
        }
    }

    /// <inheritdoc/>
    public override async Task ServerStreamingServerHandler<TRequest, TResponse>(
        TRequest request,
        IServerStreamWriter<TResponse> responseStream,
        ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation
    )
    {
        try
        {
            await base.ServerStreamingServerHandler(request, responseStream, context, continuation);
        }
        catch (Exception baseException)
        {
            throw DefineException(baseException);
        }
    }

    /// <summary>
    /// Конвертирование ошибок в <see cref="RpcException"/>.
    /// </summary>
    private RpcException DefineException(Exception exception)
    {
        logger.LogError(exception, "Ошибка при обработке GRPC-метода.");

        if (exception is RpcException rpcException)
        {
            return rpcException;
        }

        var statusCode = statusCodeDefiner?.GetStatusCodeByException(exception) ?? StatusCode.Internal;

        return new RpcException(new Status(statusCode, exception.Message, exception));
    }

}
