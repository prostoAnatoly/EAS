using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Shared.Common.Extensions;
using System.Diagnostics;
using System.Text.Json;

namespace Shared.Infrastructure.Behaviors.Logging;

public class LoggingBehavior<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    private const string OPERATION_TYPE_NAME = "OperationType";
    private const string OPERATION_ID_NAME = "OperationId";

    private readonly ILogger<LoggingBehavior<TIn, TOut>> logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TIn, TOut>> logger)
    {
        this.logger = logger;
    }

    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        var operationId = Guid.NewGuid();
        var operationType = request.GetType().Name;

        using (LogContext.PushProperty(OPERATION_ID_NAME, operationId))
        using (LogContext.PushProperty(OPERATION_TYPE_NAME, operationType))
        {
            logger.LogInformation("Начата операция {OPERATION_TYPE_NAME} с ИД {OPERATION_ID_NAME}",
                operationType, operationId);

            TOut result;
            var sw = Stopwatch.StartNew();
            try
            {
                result = await next();
            }
            catch
            {
                LogRequest(request);

                throw;
            }
            finally
            {
                sw.Stop();
                logger.LogInformation("Завершена операция {OPERATION_TYPE_NAME} с ИД {OPERATION_ID_NAME}." +
                    " Исполнена за {OPERATION_LEAD_TIME}",
                    operationType, operationId, sw.Elapsed);
            }

            return result;
        }
    }

    private void LogRequest(TIn request)
    {
        string requestData;
        try
        {
            requestData = JsonSerializer
                .Serialize(request)
                .SubstringSmart(1000);
        }
        catch (Exception e)
        {
            requestData = $"невозможно сериализовать входящие сообщение: {e.Message}";
        }
        logger.LogDebug("Тело запроса {Request}.", requestData);
    }

}