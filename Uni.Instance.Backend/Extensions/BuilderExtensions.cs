using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using NSwag.Generation.AspNetCore;

using Uni.Instance.Backend.Configuration;
using Uni.Instance.Backend.Data;
using Uni.Instance.Backend.Endpoints.Auth.Services;
using Uni.Instance.Backend.Endpoints.Internal.Services;


namespace Uni.Instance.Backend.Extensions;

public static class BuilderExtensions {
  public static void ConfigureFastEndpoints(this WebApplicationBuilder builder) {
    var signingKey = builder
      .Configuration
      .GetRequiredSection("Security")
      .GetRequiredSection("SigningKey").Value;

    ArgumentNullException.ThrowIfNull(signingKey);

    builder.Services.AddAuthorization();
    builder.Services.AddFastEndpoints();
    builder.Services.AddJWTBearerAuth(signingKey);
  }

  public static void ConfigureDatabase(this WebApplicationBuilder builder) {
    builder.Services.AddDbContextPool<AppDbContext>(
      options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
  }

  public static void MapConfiguration(this WebApplicationBuilder builder) {
    builder.Services.Configure<SecurityConfiguration>(builder.Configuration.GetRequiredSection("Security"));
    builder.Services.AddSingleton(resolver =>
      resolver.GetRequiredService<IOptions<SecurityConfiguration>>().Value);
  }

  public static void RegisterServices(this WebApplicationBuilder builder) {
    builder.Services.AddSingleton<PingService>();
    builder.Services.AddTransient<AuthService>();
    builder.Services.AddTransient<IClaimsTransformation, UserRoleHydrator>();
  }

  public static void ConfigureSwaggerDocuments(this WebApplicationBuilder builder) {
    builder.Services.SwaggerDocument(o => {
      o.AutoTagPathSegmentIndex = 0;
      o.MaxEndpointVersion = 2;
      o.ShortSchemaNames = true;
      o.ShowDeprecatedOps = true;
      o.TagDescriptions = Tags;
      o.DocumentSettings = s => DocumentSettings(s, "v2");
    });
    return;

    void DocumentSettings(AspNetCoreOpenApiDocumentGeneratorSettings s, string version) {
      const string title = "UNI API";
      s.Title = title;
      s.Description = "API of the learning management system \"UNI\"";
      s.DocumentName = $"{title} {version}";
      s.Version = version;
    }

    void Tags(IDictionary<string, string> t) {
      t[ApiTags.Internal.Tag] = ApiTags.Internal.Description;
      t[ApiTags.Auth.Tag] = ApiTags.Auth.Description;
    }
  }
}