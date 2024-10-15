using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Common.Extensions;
using Shared.Rest.Common.Models;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Shared.Rest.Common.Middlewares;

/// <summary>
/// Промежуточный слой для обработки не предвиденных ошибок в restfull API
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ErrorHandlingMiddleware> logger;
    private readonly IHttpStatusCodeDefiner httpStatusCodeDefiner;
    private readonly JsonSerializerOptions jsonSerializerOptions;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ErrorHandlingMiddleware"/>
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger,
        IHttpStatusCodeDefiner httpStatusCodeDefiner)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
        this.logger = logger;
        this.httpStatusCodeDefiner = httpStatusCodeDefiner;
        jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    /// <summary>
    /// Метод встраивающийся в цепочку промежуточных слоёв
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            if (IsLoggingBody(context))
            {
                context.Request.EnableBuffering();
            }

            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static bool IsLoggingBody(HttpContext context)
    {
        if (context.Request == null) { return false; }
        if (context.Request.Method == HttpMethods.Get) { return false; }

        return true;
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var message = $"Path: {context.Request.Path}";

        if (IsLoggingBody(context))
        {
            string body;
            try
            {
                var maxLenBody = 1000;
                var stream = new StreamReader(context.Request.Body);
                stream.BaseStream.Seek(0, SeekOrigin.Begin);
                body = stream.ReadToEnd().SubstringSmart(maxLenBody, $"Тело ответа превышает {maxLenBody}");
            }
            catch (Exception ex)
            {
                body = $"Невозможно прочитать тело запроса для: {ex.Message}";
            }
            message += $"; Body: {body}";
        }

        logger?.LogError(exception, message);

        // Подготавливаем ответ для клиента
        var statusCode = httpStatusCodeDefiner.GetStatusCodeByException(exception);
        var errorMessage = statusCode == HttpStatusCode.InternalServerError
            ? "Ошибка сервера, повторите операцию позже"
            : exception.Message;

        var problem = new ApiResponse<ResponseErrorBadRequest>(new ResponseErrorBadRequest
        {
            MessageBase = errorMessage,
        });

        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var json = JsonSerializer.Serialize(problem, jsonSerializerOptions);

        return context.Response.WriteAsync(json);
    }
}
