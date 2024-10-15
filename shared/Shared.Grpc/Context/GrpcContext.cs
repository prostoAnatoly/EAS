using Microsoft.AspNetCore.Http;

namespace Shared.Grpc.Context;

/// <summary>
/// Контекст gRPC-запроса.
/// </summary>
public class GrpcContext
{

    public HttpContext? HttpContext { get; internal set; }

}
