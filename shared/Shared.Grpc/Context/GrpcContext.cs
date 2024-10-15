using Microsoft.AspNetCore.Http;

namespace Shared.Grpc.Context;

/// <summary>
/// Контекст запроса GRPC.
/// </summary>
public class GrpcContext
{

    /// <summary>
    /// Заголовки запроса GRPC.
    /// </summary>
    public HttpContext? HttpContext { get; internal set; }

}
