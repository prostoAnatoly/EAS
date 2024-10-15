using System.Reflection;

namespace Shared.Rest.Common.WebApiMethod;

/// <summary>
/// Инфомрация об веб-API методе
/// </summary>
class WebApiMethodInfo
{
    /// <summary>
    /// Информация об API-методе
    /// </summary>
    public MemberInfo MemberInfo { get; }

    /// <summary>
    /// Параметры вызова метода
    /// </summary>
    public object?[] Parameters { get; }

    public WebApiMethodInfo(MemberInfo memberInfo, object?[] parameters)
    {
        MemberInfo = memberInfo;
        Parameters = parameters;
    }
}