using Aip.Instance.Backend.Extensions;

using FastEndpoints.Swagger;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;


var builder = WebApplication.CreateBuilder();

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console()
  .WriteTo.OpenTelemetry(opts => {
    opts.Endpoint = "http://localhost:4317";
    opts.Protocol = OtlpProtocol.Grpc;
    opts.ResourceAttributes = new Dictionary<string, object> {
      ["service.name"] = "AIP API",
    };
  })
  .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
  .MinimumLevel.Override("Default", LogEventLevel.Debug)
  .MinimumLevel.Override("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogEventLevel.Verbose)
  .CreateLogger();

builder.Host.UseSerilog();
builder.ConfigureFastEndpoints();
builder.ConfigureDatabase();
builder.MapConfiguration();
builder.RegisterServices();
builder.ConfigureSwaggerDocuments();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.ConfigureAuthorization();
app.ConfigureFastEndpoints();
app.UseSwaggerGen(c => { c.Path = "/swagger/{documentName}/swagger.json"; });
app.ApplyMigrations();

app.Run();