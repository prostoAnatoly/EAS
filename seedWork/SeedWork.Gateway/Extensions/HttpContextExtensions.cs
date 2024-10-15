using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Shared.Grpc.Models;
using System.Net.Mime;

namespace SeedWork.Gateway.Extensions;

/// <summary>
/// Расширение для типа <see cref="HttpContext"/>
/// </summary>
public static class HttpContextExtensions
{

    public static async Task PushFileFromGrpc<TGrpcResponse>(this HttpContext httpContext,
        IAsyncEnumerable<StreamChunk<TGrpcResponse>> chunks, Func<TGrpcResponse, string> getFileName)
        where TGrpcResponse : class
    {
        var response = httpContext.Response;
        var isFirst = true;
        var stream = response.BodyWriter.AsStream();
        await foreach (var chunk in chunks)
        {
            if (isFirst)
            {
                isFirst = false;
                response.ContentType = MediaTypeNames.Application.Octet;

                if (chunk.Meta != null)
                {
                    var cd = new ContentDispositionHeaderValue("attachment");
                    var fileName = getFileName(chunk.Meta);
                    cd.SetHttpFileName(fileName);

                    response.Headers[HeaderNames.ContentDisposition] = cd.ToString();
                }
            }

            stream.Write(chunk.Chunk);
        }
    }
}