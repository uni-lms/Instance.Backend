global using FastEndpoints;
global using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o => o.DocumentSettings = s =>
{
    s.Title = "UNI API";
    s.Version = "v1";
});

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();

app.Run();