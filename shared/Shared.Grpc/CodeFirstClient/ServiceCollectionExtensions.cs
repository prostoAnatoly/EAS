using Grpc.AspNetCore.Server;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Server;
using Shared.Grpc.CodeFirstClient.Surrogates;
using Shared.Grpc.Context;
using Shared.Grpc.Extensions;

namespace Shared.Grpc.CodeFirstClient
{
    /// <summary>
    /// Расширения для регистрации служб.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление GRPC-клиента.
        /// </summary>
        public static IServiceCollection AddCustomeGrpcClient<TService>(
            this IServiceCollection services,
            string grpcEndpoint,
            params Type[] interceptors
        )
            where TService : class
        {
            // Первым делом регистрируем указанные интерсепторы.
            foreach (var interceptor in interceptors)
            {
                services.AddSingleton(interceptor);
            }

            // Далее регистрируем непосредственно GRPC-клиент.
            services.AddSingleton(serviceProvider =>
            {
                // Создаем канал для GRPC-клиента
                var channel = GrpcChannel.ForAddress(grpcEndpoint);
                var invoker = channel.CreateCallInvoker();

                // При наличии интерсепторов, навешиваем их на пайплайн.
                foreach (var interceptor in interceptors)
                {
                    var newInterceptor = (Interceptor)serviceProvider.GetService(interceptor)!;
                    invoker = invoker.Intercept(newInterceptor);
                }

                // Формируем клиент и возвращаем в качестве результата сервис.
                var client = invoker.CreateGrpcService<TService>();

                return client;
            });
            
            SurrogateRegistrator.Register();

            return services;
        }

        /// <summary>
        /// Добавление GRPC-сервера.
        /// </summary>
        public static IServiceCollection AddCodeFirstGrpcServer(
            this IServiceCollection services,
            Action<GrpcServiceOptions>? configureOptions = null
        )
        {
            services.AddScoped<GrpcContext>();
            services.AddCodeFirstGrpc(innerConfigureOptions =>
            {
                innerConfigureOptions.Interceptors.AddExceptionInterceptor();
                configureOptions?.Invoke(innerConfigureOptions);
            });
            
            SurrogateRegistrator.Register();
            
            return services;
        }
    }
}
