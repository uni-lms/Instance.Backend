global using FastEndpoints;
global using FastEndpoints.Swagger;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.SwaggerDocument(o => o.DocumentSettings = s =>
{
    s.Title = "UNI API";
    s.DocumentName = "Version 1";
    s.Version = "v1";
});

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "v";
    c.Versioning.PrependToRoute = true;
});
app.UseSwaggerGen();

app.Run();