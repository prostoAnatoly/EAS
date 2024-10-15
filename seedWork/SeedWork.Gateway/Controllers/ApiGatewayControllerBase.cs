using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Rest.Common.Controllers;
using Shared.Rest.Common.Models;
using Shared.Rest.Common.WebApiMethod.Attributes;
using System.Net;

namespace SeedWork.Gateway.Controllers
{
    public abstract class ApiGatewayControllerBase : ApiControllerBase
    {
        protected readonly IMapper _mapper;

        protected ApiGatewayControllerBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает ответ 200
        /// </summary>
        /// <typeparam name="TOutResponse">Тип полезной нагрузки в ответе</typeparam>
        /// <param name="response">Ответ</param>
        [WebApiResponse(HttpStatusCode.OK)]
        protected OkObjectResult GetOkWithMapping<TOutResponse>(object response)
            where TOutResponse : class
        {
            return Ok(new ApiResponse<TOutResponse>(_mapper.Map<TOutResponse>(response)));
        }
    }
}
