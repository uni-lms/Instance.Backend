using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSwag.Generation.AspNetCore;
using Uni.Backend.Configuration;
using Uni.Backend.Data;
using Uni.Backend.Modules.Auth.Services;
using Uni.Backend.Modules.Static.Services;

namespace Uni.Backend.Extensions;

public static class BuilderExtensions
{
    public static void ConfigureFastEndpoints(this WebApplicationBuilder builder)
    {
        var signingKey = builder
            .Configuration
            .GetRequiredSection("Security")
            .GetRequiredSection("SigningKey").Value;

        ArgumentNullException.ThrowIfNull(signingKey);

        builder.Services.AddFastEndpoints();
        builder.Services.AddJWTBearerAuth(signingKey);
    }

    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContextPool<AppDbContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
    }

    public static void MapConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SecurityConfiguration>(builder.Configuration.GetRequiredSection("Security"));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<SecurityConfiguration>>().Value);
    }

    public static void RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<StaticService>();
    }

    public static void ConfigureSwaggerDocuments(this WebApplicationBuilder builder)
    {
        void Tags(Dictionary<string, string> t)
        {
            t["Roles"] = "API of users' roles (student, tutor, administrator)";
            t["Courses"] = "API of courses (groups of materials and assignments)";
            t["Groups"] = "API of groups of students";
            t["Users"] = "API of users";
            t["Auth"] = "API for signing in/up users";
            t["Static"] = "API for management static files";
        }

        void DocumentSettings(AspNetCoreOpenApiDocumentGeneratorSettings s, string version)
        {
            s.Title = "UNI API";
            s.Description = "API of the learning management system \"UNI\"";
            s.DocumentName = version;
            s.Version = version;
        }

        builder.Services.SwaggerDocument(o =>
        {
            o.AutoTagPathSegmentIndex = 0;
            o.MaxEndpointVersion = 1;
            o.TagDescriptions = Tags;
            o.DocumentSettings = s => DocumentSettings(s, "v1");
        });
    }
}