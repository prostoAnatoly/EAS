using Microsoft.AspNetCore.Mvc;
using Shared.Rest.Common.Models;
using Shared.Rest.Common.WebApiMethod.Attributes;
using System.Net;
using System.Net.Mime;

namespace Shared.Rest.Common.Controllers;

/// <summary>
/// Базовый контроллер API
/// </summary>
[ApiController, Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
public abstract class ApiControllerBase : ControllerBase
{

    /// <summary>
    /// Возвращает ответ 200
    /// </summary>
    /// <typeparam name="T">Тип полезной нагрузки в ответе</typeparam>
    /// <param name="response">Ответ</param>
    [WebApiResponse(HttpStatusCode.OK)]
    protected OkObjectResult GetOk<T>(T response)
        where T : class
    {
        return Ok(new ApiResponse<T>(response));
    }

    /// <summary>
    /// Возвращает ответ 201
    /// </summary>
    /// <typeparam name="T">Тип полезной нагрузки в ответе</typeparam>
    /// <param name="response">Ответ</param>
    [WebApiResponse(HttpStatusCode.Created)]
    protected CreatedAtRouteResult GetCreated<T>(EntityOfCreate<T> response)
        where T : class
    {
        return CreatedAtRoute(response.RouteName, response.RouteValues, new ApiResponse<T>(response.Payload));
    }

    /// <summary>
    /// Возвращает ответ 400
    /// </summary>
    /// <param name="message">Сообщение для клиента</param>
    [WebApiResponse(HttpStatusCode.BadRequest)]
    protected BadRequestObjectResult GetBadRequest(string message)
    {
        return BadRequest(new ApiResponse<ResponseErrorBadRequest>(new ResponseErrorBadRequest()
        {
            MessageBase = message,
        }));
    }

    /// <summary>
    /// Возвращает ответ 404
    /// </summary>
    /// <param name="message">Сообщение для клиента</param>
    [WebApiResponse(HttpStatusCode.NotFound)]
    protected NotFoundObjectResult GetNotFound(string message)
    {
        return NotFound(new ApiResponse<ResponseErrorModelBase>(new ResponseErrorModelBase
        {
            MessageBase = message,
        }));
    }

    /// <summary>
    /// Возвращает ответ 409
    /// </summary>
    /// <param name="message">Сообщение для клиента</param>
    [WebApiResponse(HttpStatusCode.Conflict)]
    protected ConflictObjectResult GetConflict(string message)
    {
        return Conflict(new ApiResponse<ResponseErrorModelBase>(new ResponseErrorModelBase
        {
            MessageBase = message,
        }));
    }

}