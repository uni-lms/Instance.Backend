using Aip.Instance.Backend.Api.Auth.Services;
using Aip.Instance.Backend.Api.Content.File.Services;
using Aip.Instance.Backend.Api.Flows.Services;
using Aip.Instance.Backend.Api.Internships.Services;
using Aip.Instance.Backend.Api.Sections.Services;
using Aip.Instance.Backend.Api.Users.Services;
using Aip.Instance.Backend.Configuration.Models;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data;

using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using NSwag.Generation.AspNetCore;


namespace Aip.Instance.Backend.Extensions;

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
    builder.Services.AddTransient<AuthService>();
    builder.Services.AddTransient<CourseSectionsService>();
    builder.Services.AddTransient<FlowsService>();
    builder.Services.AddTransient<UsersService>();
    builder.Services.AddTransient<InternshipsService>();
    builder.Services.AddTransient<ContentFileService>();
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
  }

  private static void DocumentSettings(AspNetCoreOpenApiDocumentGeneratorSettings s, string version) {
    const string title = "UNI API";
    s.Title = title;
    s.Description = "API of the learning management system \"UNI\"";
    s.DocumentName = $"{title} {version}";
    s.Version = version;
  }

  private static void Tags(IDictionary<string, string> t) {
    t[ApiTags.Internal.Tag] = ApiTags.Internal.Description;
    t[ApiTags.Auth.Tag] = ApiTags.Auth.Description;
    t[ApiTags.Sections.Tag] = ApiTags.Sections.Description;
    t[ApiTags.Flows.Tag] = ApiTags.Flows.Description;
    t[ApiTags.Users.Tag] = ApiTags.Users.Description;
    t[ApiTags.Internships.Tag] = ApiTags.Internships.Description;
    t[ApiTags.FileContent.Tag] = ApiTags.FileContent.Description;
  }
}