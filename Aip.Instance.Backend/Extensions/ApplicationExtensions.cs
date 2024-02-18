using System.Net;

using Aip.Instance.Backend.Data;
using Aip.Instance.Backend.Data.Common;

using FastEndpoints;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

using Serilog;

using ILogger = Microsoft.Extensions.Logging.ILogger;


namespace Aip.Instance.Backend.Extensions;

#pragma warning disable S2094
#pragma warning restore S2094

public static class ApplicationExtensions {
  public static void ConfigureAuthorization(this WebApplication app) {
    app.UseAuthentication();
    app.UseAuthorization();
  }

  public static void ConfigureFastEndpoints(this WebApplication app) {
    app.UseFastEndpoints(c => {
      c.Versioning.Prefix = "v";
      c.Versioning.PrependToRoute = true;
      c.Endpoints.ShortNames = true;
    });
  }

  public static void ApplyMigrations(this WebApplication app) {
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();

    Log.Logger.Information("Checking if has pending migrations...");

    var pendingMigrations = context.Database.GetPendingMigrations().ToList();
    if (pendingMigrations.Count == 0) {
      return;
    }

    Log.Logger.Information(
      "Found pending migrations: {PendingMigrations}, migrating...",
      pendingMigrations);
    context.Database.Migrate();
  }

  public static IApplicationBuilder UseCustomExceptionHandler(
    this IApplicationBuilder app,
    ILogger? logger = null,
    bool logStructuredException = false
  ) {
    app.UseExceptionHandler(errApp => {
      errApp.Run(async ctx => {
        var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
        if (exHandlerFeature is not null) {
          logger ??= ctx.Resolve<ILogger<ExceptionHandler>>();
          var http = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
          var type = exHandlerFeature.Error.GetType().Name;
          var error = exHandlerFeature.Error.Message;
          var msg =
            """
            =================================
            {http}
            TYPE: {type}
            REASON: {error}
            ---------------------------------
            {exHandlerFeature.Error.StackTrace}
            """;

          if (logStructuredException) {
            logger.LogError("{Http}{Type}{Reason}{Exception}", http, type, error, exHandlerFeature.Error);
          }
          else {
            logger.LogError(msg, http, type, error, exHandlerFeature.Error.StackTrace);
          }

          ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          ctx.Response.ContentType = "application/problem+json";
          await ctx.Response.WriteAsJsonAsync(new Error {
            Reason = error,
            Code = ctx.Response.StatusCode,
          });
        }
      });
    });

    return app;
  }
}