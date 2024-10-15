using Grpc.Core;
using Shared.App.Exceptions;
using Shared.Domain.Exceptions;

namespace SeedWork.Infrastructure.Extensions;

public static class GrpcExtensions
{

    private static Exception MapException(RpcException rpcEx) => rpcEx.StatusCode switch
    {
        StatusCode.InvalidArgument => new BadRequestException(rpcEx.Status.Detail),
        StatusCode.NotFound => new EntityNotFoundException(rpcEx.Status.Detail),
        StatusCode.AlreadyExists => new DbForeignKeyException(rpcEx.Status.Detail),
        _ => rpcEx,
    };

    private static Exception MapException(Func<RpcException, Exception?>? customMapException, RpcException rpcEx)
    {
        var customException = customMapException?.Invoke(rpcEx);

        return customException ?? MapException(rpcEx);
    }

    public static async Task<TResult> TryCatchGrpcToAppException<TResult>(this Task<TResult> task,
        Func<RpcException, Exception?>? customMapException = null)
    {
        try
        {
            return await task;
        }
        catch (RpcException rpcEx)
        {
            throw MapException(customMapException, rpcEx);
        }
    }

    public static async Task TryCatchGrpcToAppException(this Task task,
        Func<RpcException, Exception?>? customMapException = null)
    {
        try
        {
            await task;
        }
        catch (RpcException rpcEx)
        {
            throw MapException(customMapException, rpcEx);
        }
    }

    public static async IAsyncEnumerable<TResult> TryCatchGrpcToAppException<TResult>(this IAsyncEnumerable<TResult> stream,
        Func<RpcException, Exception?>? customMapException = null) where TResult : class
    {
        var enumerator = stream.GetAsyncEnumerator();
        TResult? result;
        bool hasResult = true;

        while (hasResult)
        {
            try
            {
                hasResult = await enumerator.MoveNextAsync();

                result = hasResult ? enumerator.Current : null;
            }
            catch (RpcException rpcEx)
            {
                throw MapException(customMapException, rpcEx);
            }

            if (result != null)
            {
                yield return result;
            }
        }
    }
}
