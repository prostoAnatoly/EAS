using FilesStorage.App;
using FilesStorage.Domain;
using FilesStorage.Grpc.Services;
using FilesStorage.Infrastructure;
using FilesStorage.Infrastructure.Persistence;
using Shared.Automapper;
using Shared.Grpc.CodeFirstClient;
using Shared.Infrastructure.HealthChecks;
using Shared.Persistence;
using System.Reflection;
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MigrateDatabase<FilesStorageDatabaseContext>();

// Configure the HTTP request pipeline.
app.MapGrpcService<FilesStorageServiceGrpc>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.MapDefaultHealthChecks();

app.Run();
