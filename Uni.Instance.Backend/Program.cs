using FastEndpoints.Swagger;

using Serilog;
using Serilog.Events;

using Uni.Instance.Backend.Extensions;


var builder = WebApplication.CreateBuilder();

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
builder.ConfigureSwaggerDocuments();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.ConfigureAuthorization();
app.ConfigureFastEndpoints();
app.UseSwaggerGen(c => { c.Path = "/swagger/{documentName}/swagger.json"; });
app.ApplyMigrations();

app.Run();
