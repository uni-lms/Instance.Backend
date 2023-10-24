using FastEndpoints;
using FastEndpoints.Swagger;

using Serilog;
using Serilog.Events;

using Uni.Backend.Extensions;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console()
  .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
  .MinimumLevel.Override("Default", LogEventLevel.Debug)
  .MinimumLevel.Override("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogEventLevel.Verbose)
  .CreateLogger();

builder.Host.UseSerilog();

builder.ConfigureFastEndpoints();
builder.ConfigureMassTransit();
builder.ConfigureDatabase();
builder.MapConfiguration();
builder.RegisterDependencies();
builder.ConfigureSwaggerDocuments();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.ConfigureAuthorization();
app.UseDefaultExceptionHandler();
app.ConfigureFastEndpoints();
app.UseSwaggerGen(c => {
  c.Path = "/api/swagger/{documentName}/swagger.json";
});
app.ApplyMigrations();

app.Run();