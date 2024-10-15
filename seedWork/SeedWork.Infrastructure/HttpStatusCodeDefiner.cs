using FluentValidation;
using Shared.App.Exceptions;
using Shared.Domain.Exceptions;
using Shared.Rest.Common;
using System.Net;

namespace SeedWork.Infrastructure;

internal class HttpStatusCodeDefiner : IHttpStatusCodeDefiner
{
    public HttpStatusCode GetStatusCodeByException(Exception exception) => exception switch
    {
        BadRequestException or ValidationException or DomainException => HttpStatusCode.BadRequest,
        EntityNotFoundException => HttpStatusCode.NotFound,
        DbForeignKeyException => HttpStatusCode.Conflict,
        _ => HttpStatusCode.InternalServerError,
    };
}
