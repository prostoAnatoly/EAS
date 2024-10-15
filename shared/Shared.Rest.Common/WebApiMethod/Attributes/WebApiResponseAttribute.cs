using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Shared.Rest.Common.WebApiMethod.Attributes
{
    /// <summary>
    /// Задаёт информацию об ответе, связанным с <see cref="IActionResult"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class WebApiResponseAttribute : Attribute
    {

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WebApiResponseAttribute"/>
        /// </summary>
        /// <param name="statusCode">Код состояния HTTP</param>
        public WebApiResponseAttribute(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Код состояния HTTP
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

    }
}