using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Uni.Backend.Data;

namespace Uni.Backend.Extensions;

public static class ApplicationExtensions
{
    public static void ConfigureAuthorization(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void ConfigureFastEndpoints(this WebApplication app)
    {
        app.UseFastEndpoints(c =>
        {
            c.Versioning.Prefix = "v";
            c.Versioning.PrependToRoute = true;
            c.Endpoints.ShortNames = true;
        });
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AppDbContext>();

        Log.Logger.Information("Checking if has pending migrations...");

        if (!context.Database.GetPendingMigrations().Any()) return;
        Log.Logger.Information(
            "Found pending migrations: {GetPendingMigrations}, migrating...",
            context.Database.GetPendingMigrations());
        context.Database.Migrate();
    }
}