using FastEndpoints;
using FastEndpoints.Swagger;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddFastEndpoints();
builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.SwaggerDocument(o =>
{
    o.AutoTagPathSegmentIndex = 0;
    o.MaxEndpointVersion = 1;
    o.TagDescriptions = t =>
    {
        t["Roles"] = "API of users' roles (student, tutor, administrator)";
        t["Courses"] = "API of courses (groups of materials and assignments)";
        t["Groups"] = "API of groups of students";
        t["Users"] = "API of users";
    };
    o.DocumentSettings = s =>
    {
        s.Title = "UNI API";
        s.DocumentName = "Version 1";
        s.Version = "v1";
    };
});

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseAuthorization();
app.UseDefaultExceptionHandler();
app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "v";
    c.Versioning.PrependToRoute = true;
});
app.UseSwaggerGen();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<AppDbContext>();

Log.Logger.Information("Checking if has pending migrations...");

if (context.Database.GetPendingMigrations().Any())
{
    Log.Logger.Information(
        "Found pending migrations: {GetPendingMigrations}, migrating...",
        context.Database.GetPendingMigrations());
    context.Database.Migrate();
}

app.Run();