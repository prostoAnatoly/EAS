using FluentValidation;
using Grpc.Core;
using Shared.App.Exceptions;
using Shared.Domain.Exceptions;
using Shared.Grpc;

namespace SeedWork.Infrastructure;

internal class GrpcStatusCodeDefiner : IGrpcStatusCodeDefiner
{
    public StatusCode GetStatusCodeByException(Exception exception) => exception switch
    {
        BadRequestException or ValidationException or DomainException => StatusCode.InvalidArgument,
        EntityNotFoundException => StatusCode.NotFound,
        DbForeignKeyException => StatusCode.AlreadyExists,
        _ => StatusCode.Internal,
    };
}