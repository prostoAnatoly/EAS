using Shared.IL.Utils.ILReader;
using Shared.Rest.Common.WebApiMethod.Attributes;
using System.Reflection;
using System.Reflection.Emit;

namespace Shared.Rest.Common.WebApiMethod;

/// <inheritdoc />
public class WebApiMethodReader : IWebApiMethodReader
{
    private readonly MemberInfo[] includeMethods;
    private readonly IWebApiResponseCreator webApiResponseCreator;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="WebApiMethodReader"/>
    /// </summary>
    /// <param name="webApiResponseCreator">Создатель ответов для веб-API</param>
    public WebApiMethodReader(IWebApiResponseCreator webApiResponseCreator)
    {
        this.webApiResponseCreator = webApiResponseCreator ?? throw new ArgumentNullException(nameof(webApiResponseCreator));
        includeMethods = webApiResponseCreator.MethodsGeneratingRealResponse;
    }

    /// <inheritdoc />
    public WebApiResponseInfo[] GetWebApiResponses(MethodInfo member)
    {
        var responses = new List<WebApiResponseInfo>();

        var methodNames = includeMethods.Select(m => m.Name).ToArray();
        var returnMethods = GetReturnMethods(methodNames, member);
        foreach (var rMethod in returnMethods)
        {
            var name = rMethod.MemberInfo.Name;
            var includeMi = Array.Find(includeMethods, m => m.Name == name)
                ?? throw new NotSupportedException($"Неподдерживаемый возвращаемый метод {name} в методе {member.Name}");
            var attr = includeMi.GetCustomAttributes<WebApiResponseAttribute>().FirstOrDefault()
                ?? throw new InvalidOperationException($"Отсутствует атрибут {typeof(WebApiResponseAttribute).Name} для метода {name}");

            var statusCode = (int)attr.StatusCode;
            var respResult = responses.Find(r => r.StatusCode == statusCode);
            if (respResult == null)
            {
                var mi = rMethod.MemberInfo as MethodInfo;
                if (mi == null) { continue; }

                var respWebApi = webApiResponseCreator.GetWebApiResponse(statusCode);
                if (respWebApi == null) { continue; }

                var payloadType = respWebApi.ResponseType;
                if (payloadType.IsGenericType)
                {
                    var gArg = payloadType.GetGenericArguments();
                    if (gArg.Length == 2 && gArg[0].FullName == null)
                    {
                        var miParams = mi.GetParameters();
                        var miGenericArguments = mi.GetGenericArguments();
                        var isOk = false;
                        var isErr = false;
                        if (!mi.IsGenericMethod
                            || (!(isOk = (miGenericArguments.Length == 2 && miParams.Length == 2 && miParams[1].ParameterType.IsEnum))
                                && !(isErr = (miGenericArguments.Length == 1 && miParams.Length > 0 && miParams[0].ParameterType.IsEnum)))
                            )
                        {
                            throw new InvalidOperationException($"Метод {name} не соответствует шаблону");
                        }

                        var agr0 = (respWebApi.GenericArgumentZeroType != null && respWebApi.GenericArgumentZeroType.Length > 0
                            ? respWebApi.GenericArgumentZeroType[0] : null) ?? miGenericArguments[0];

                        if (isOk)
                        {
                            payloadType = payloadType.MakeGenericType(miGenericArguments[0], miParams[1].ParameterType);
                        }

                        if (isErr)
                        {
                            payloadType = payloadType.MakeGenericType(agr0, miParams[0].ParameterType);
                        }
                    }
                }

                respResult = new WebApiResponseInfo
                {
                    StatusCode = statusCode,
                    ResponseType = payloadType,
                };
                responses.Add(respResult);
            }

            var respCode = (Enum?)rMethod.Parameters[0];
            if (respCode != null && !respResult.ResponseCodes.Contains(respCode))
            {
                respResult.ResponseCodes.Add(respCode);
            }
        }

        return [.. responses];
    }

    /// <inheritdoc cref="GetWebApiResponses"/>
    /// <param name="includeMethodNames">Список имён методов, которые требуется анализировать</param>
    /// <param name="member">Информация о методе для разбора</param>
    /// <inheritdoc cref="GetWebApiResponses"/>
    private static WebApiMethodInfo[] GetReturnMethods(string[] includeMethodNames, MethodInfo member)
    {
        var reader = new MethodBodyReader(member);
        var instructions = reader.GetILInstructions();

        var res = new List<WebApiMethodInfo>();
        for (var i = 0; i < instructions.Length; i++)
        {
            var ins = instructions[i];
            var memInfo = ins.Operand as MethodInfo;

            if (ins.Code != OpCodes.Call)
            {
                continue;
            }
            if (memInfo == null || !member.ReturnType.IsAssignableFrom(memInfo.ReturnType))
            {
                continue;
            }
            if (includeMethodNames != null &&
                includeMethodNames.Length != 0 && !includeMethodNames.Contains(memInfo.Name))
            {
                continue;
            }

            var mi = ins.Operand as MemberInfo;
            if (mi == null) { continue; }

            var ps = ((MethodInfo)mi).GetParameters();
            if (ps.Length == 2 && (ps[1].ParameterType.IsEnum || ps[0].ParameterType.IsEnum))
            {
                int indParamAsEnum;
                if (!ps[0].ParameterType.IsEnum && ps[1].ParameterType.IsEnum)
                {
                    indParamAsEnum = 1;
                }
                else if (ps[0].ParameterType.IsEnum && !ps[1].ParameterType.IsEnum)
                {
                    indParamAsEnum = 0;
                }
                else
                {
                    break;
                }

                var insLdcI4 = instructions[i - (2 - indParamAsEnum)];
                var ldc_i4 = insLdcI4.Code; // тут лежит значение Enum - как литера

                int val;
                if (ldc_i4 == OpCodes.Ldc_I4_0)
                {
                    val = 0;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_1)
                {
                    val = 1;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_2)
                {
                    val = 2;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_3)
                {
                    val = 3;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_4)
                {
                    val = 4;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_5)
                {
                    val = 5;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_6)
                {
                    val = 6;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_7)
                {
                    val = 7;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_8)
                {
                    val = 8;
                }
                else if (ldc_i4 == OpCodes.Ldc_I4_S || ldc_i4 == OpCodes.Ldc_I4)
                {
                    val = Convert.ToInt32(insLdcI4.Operand);
                }
                else
                {
                    break;
                }

                var parameters = new object?[] { ConvertToEnum(val, ps[indParamAsEnum].ParameterType) };
                res.Add(new WebApiMethodInfo(mi, parameters));
            }
        }

        return [.. res];
    }

    private static object? ConvertToEnum(object value, Type targetType)
    {
        if (value == null) { return null; }

        if (targetType.IsEnum)
        {
            return Enum.ToObject(targetType, value);
        }

        if (value.GetType().IsEnum)
        {
            return Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
        }

        return null;
    }
}