using Organizations.App.Dtos;
using Organizations.App.Features;
using Organizations.Grpc.Contracts;
using Organizations.Grpc.Contracts.Arguments;
using Organizations.Grpc.Contracts.Responses;
using ProtoBuf.Grpc;
using SeedWork.Infrastructure.Helpers;

namespace Organizations.Grpc.Services;

public class OrganizationsServiceGrpc : IOrganizationsServiceGrpc
{
    private readonly MediatorHelper _mediatorHelper;

    public OrganizationsServiceGrpc(MediatorHelper mediatorHelper)
    {
        _mediatorHelper = mediatorHelper;
    }

    public async Task<CreateOrganizationGrpcResponse> CreateOrganization(CreateOrganizationArgs args, CallContext context = default)
    {
        return await _mediatorHelper.CommandWithResultAndMap<CreateOrganizationCommand, CreateOrganizationResult, CreateOrganizationGrpcResponse>
            (args, context.CancellationToken);
    }

    public async Task<GetOrganizationsGrpcResponse> GetOrganizations(GetOrganizationsArgs args, CallContext context = default)
    {
        var (_, result) = await _mediatorHelper.QueryWithIntermediate<GetOrganizationsQuery, GetOrganizationsResult, GetOrganizationsGrpcResponse>
            (args, context.CancellationToken);

        return result;
    }
}
