using Eas.Gate.Ui;
using EasGateUi;
using Employees.Sdk;
using Identity.Sdk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Net.Http.Headers;
using Organizations.Sdk;
using SeedWork.Gateway;
using SeedWork.Infrastructure;
using SeedWork.Infrastructure.Extensions;
using Shared.Automapper;
using Shared.Rest.Common.Middlewares;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

services.AddControllers();
services.AddSwaggerGen();
services.AddVersioningCore();

services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName.MAIN_POLICY, builder =>
    {
        builder.AllowAnyOrigin()
            .WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Delete)
            .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, GatewayHeaders.API_VERSION);
    });

    options.AddPolicy(CorsPolicyName.ALLOW_ANONYMOUS_POLICY, builder =>
    {
        builder.AllowAnyOrigin()
            .WithMethods(HttpMethods.Get, HttpMethods.Post)
            .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, GatewayHeaders.API_VERSION);
    });
});

services.AddMvc()
    .AddMvcOptions(options => { options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider()); })
    .AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

services
    .AddIdentityClient(builder.GetEndPointValue("Services:Identity:EndPoint"))
    .AddOrganizationsClient(builder.GetEndPointValue("Services:Organizations:EndPoint"))
    .AddEmployeesClient(builder.GetEndPointValue("Services:Employees:EndPoint"));

services
    .AddInfrastructureCore()
    .RegisterAutomapper(Assembly.GetExecutingAssembly())
    .AutoMapperDisableNull();

services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser().Build());

services.AddAuthenticationBearer();

var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthentication();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(CorsPolicyName.MAIN_POLICY);

app.UseAuthorization();

app.MapControllers();

app.Run();
