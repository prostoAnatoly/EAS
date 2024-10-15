using Grpc.AspNetCore.Server;
using Shared.Grpc.Interceptors;

namespace Shared.Grpc.Extensions;

public static class InterceptorCollectionExtensions
{
    public static void AddExceptionInterceptor(this InterceptorCollection collection)
    {
        collection.Add<ServerGrpcExceptionInterceptor>();
    }
}
