using Employees.App;
using Employees.Domain;
using Employees.Grpc.Services;
using Employees.Infrastructure;
using Shared.Automapper;
using Shared.Grpc.CodeFirstClient;
using Shared.Infrastructure.HealthChecks;
using System.Reflection;
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication()
    .RegisterAutomapper(Assembly.GetExecutingAssembly())
    .AutoMapperDisableNull()
    .AddInfrastructure((options) =>
    {
        options.DatabaseConnectionString = builder.Configuration.GetConnectionString("Database") ?? string.Empty;
    })
    .AddCodeFirstGrpcServer()
    .AddGrpc();

var app = builder.Build();

app.MigrateDatabases();

app.MapGrpcService<EmployeesServiceGrpc>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.MapDefaultHealthChecks();

app.Run();
