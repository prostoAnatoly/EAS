using System.Reflection;
using System.Reflection.Emit;

namespace Shared.Rest.Common.WebApiMethod;

/// <summary>
/// Читатель веб-метода API
/// </summary>
public interface IWebApiMethodReader
{

    /// <summary>
    /// Возвращает массив методов, которые вызываются через <see cref="OpCodes.Call"/> и 
    /// имеют возвращаемый тип, который унаследован от типа метода в котором он вызывается.
    /// </summary>
    /// <param name="member">Информация о методе для разбора</param>
    /// <remarks>
    /// Производится поиск методов, который имеют в своей сигнатуре 2 входных параметра и 
    /// второй параметр является Enum с указанным значением как литера.
    /// </remarks>
    WebApiResponseInfo[] GetWebApiResponses(MethodInfo member);
}