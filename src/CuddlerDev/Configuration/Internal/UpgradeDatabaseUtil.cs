using CuddlerDev.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CuddlerDev.Configuration.Internal;

internal static class UpgradeDatabaseUtil
{
    public static void UpgradeAuthenticationDatabase(IApplicationBuilder app, IWebHostEnvironment environment, BoostConfiguration boostConfiguration, ApplicationSettings ApplicationSettings)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                    .CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<IRepository>();
        if (boostConfiguration.ResetDbOnStartup)
        {
            ResetSqlServerDb(dbContext);
        }

        if (ApplicationSettings.EnableMigrations.HasValue && ApplicationSettings.EnableMigrations.Value)
        {
            // RunSqlServerDbMigrations(dbContext, environment);
        }
    }

    private static string GetSqliteDbPath(IHostEnvironment environment, ApplicationSettings applicationSettings)
    {
        var path = Path.Combine(applicationSettings.ContentRootFolder!, "Db");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path = Path.Combine(path, "boostdc.db");
        if (!File.Exists(path))
        {
            ResetSqliteDb(environment, path);
        }

        return path;
    }

    private static void ResetSqliteDb(IHostEnvironment environment, string pathToDb)
    {
        if (File.Exists(pathToDb))
        {
            File.Delete(pathToDb);
        }

        File.Copy(Path.Combine(environment.ContentRootPath, "App_Data", "sqlite", "boostdc.db"), pathToDb);
    }

    private static void ResetSqlServerDb(IRepository dbContext)
    {
        var databaseName = dbContext.Database.GetDbConnection()
                                    .Database;
        var query = $"USE master;ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [{databaseName}] ;";
        dbContext.Database.ExecuteSqlRaw(query);
    }

    private static void RunSqliteDbMigrations(IRepository context)
    {
        context.Database.Migrate();
    }
}