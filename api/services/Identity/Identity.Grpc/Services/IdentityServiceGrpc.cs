using Identity.App.Features;
using Identity.App.Features.Login;
using Identity.App.Features.Logout;
using Identity.App.Features.Registration;
using Identity.App.Features.ValidateToken;
using Identity.Grpc.Contracts;
using Identity.Grpc.Contracts.Arguments;
using Identity.Grpc.Contracts.Responses;
using ProtoBuf.Grpc;
using SeedWork.Infrastructure.Helpers;

namespace Identity.Grpc.Services
{
    public class IdentityServiceGrpc : IIdentityServiceGrpc
    {
        private readonly MediatorHelper mediatorHelper;

        public IdentityServiceGrpc(MediatorHelper mediatorHelper)
        {
            this.mediatorHelper = mediatorHelper;
        }

        public async Task<LoginGrpcResponse> Login(LoginGrpcArgs args, CallContext context = default)
        {
            return await mediatorHelper.CommandWithResultAndMap<LoginCommand, LoginResult, LoginGrpcResponse>(args, context.CancellationToken);
        }

        public async Task Logout(LogoutGrpcArgs args, CallContext context = default)
        {
            await mediatorHelper.Command<LogoutCommand>(args, context.CancellationToken);
        }

        public async Task<RegistrationGrpcResponse> Registration(RegistrationGrpcArgs args, CallContext context = default)
        {
            return await mediatorHelper.CommandWithResultAndMap<RegistrationCommand, RegistrationResult, RegistrationGrpcResponse>(args, context.CancellationToken);
        }

        public async Task<ValidateTokenGrpcResponse> ValidateToken(ValidateTokenGrpcArgs args, CallContext context = default)
        {
            return await mediatorHelper.Query<ValidateTokenQuery, ValidateTokenResult, ValidateTokenGrpcResponse>(args, context.CancellationToken);
        }
    }
}
