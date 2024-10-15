using AutoMapper;
using Eas.Gate.Ui;
using Eas.Gate.Ui.Models.Organizations;
using EasGateUi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Organizations.Grpc.Contracts.Arguments;
using Organizations.Sdk;
using SeedWork.Gateway.Controllers;
using System.ComponentModel.DataAnnotations;

namespace EasGateUi.Controllers;

/// <summary>
/// API по работе с организациями
/// </summary>
[Route("api/organizations")]
[ApiVersion("1.0")]
[EnableCors(CorsPolicyName.MAIN_POLICY)]
[Authorize]
public class OrganizationsController : ApiGatewayControllerBase
{
    private readonly OrganizationsClient _organizationsClient;

    public OrganizationsController(OrganizationsClient organizationsClient, IMapper mapper) : base(mapper)
    {
        _organizationsClient = organizationsClient;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrganization([FromBody, Required] OrganizationCreateModel model)
    {
        var args = new CreateOrganizationArgs
        {
            CreatorId = this.GetRequiredUserId(),
            Name = model.Name,
        };
        var resp = await _organizationsClient.CreateOrganization(args);

        return GetOkWithMapping<CreateOrganizationResponse>(resp);
    }

    [HttpGet()]
    public async Task<IActionResult> GetOrganizations()
    {
        var args = new GetOrganizationsArgs
        {
            OwnerId = this.GetRequiredUserId(),
        };
        var resp = await _organizationsClient.GetOrganizations(args);

        return GetOkWithMapping<GetOrganizationsResponse>(resp);
    }
}