using FastEndpoints.Swagger;

using Microsoft.AspNetCore.HttpOverrides;

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

app.UseForwardedHeaders(new ForwardedHeadersOptions {
  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
});

app.UseSerilogRequestLogging();
app.ConfigureAuthorization();
app.UseCustomExceptionHandler();
app.ConfigureFastEndpoints();
app.UseSwaggerGen(c => { c.Path = "/swagger/{documentName}/swagger.json"; });
app.ApplyMigrations();

app.Run();