using Uni.Backend.Extensions;
using FastEndpoints;
using FastEndpoints.Swagger;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Default", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogEventLevel.Verbose)
    .CreateLogger();

builder.Host.UseSerilog();

builder.ConfigureFastEndpoints();
builder.ConfigureDatabase();
builder.MapConfiguration();
builder.RegisterDependencies();
builder.ConfigureSwaggerDocuments();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.ConfigureAuthorization();
app.UseDefaultExceptionHandler();
app.ConfigureFastEndpoints();
app.UseSwaggerGen();
app.ApplyMigrations();

app.Run();