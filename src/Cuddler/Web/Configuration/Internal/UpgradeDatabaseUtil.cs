using System.Data;
using Cuddler.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cuddler.Web.Configuration.Internal;

internal static class UpgradeDatabaseUtil
{
    public static void UpgradeAuthenticationDatabase(IApplicationBuilder app, IWebHostEnvironment environment, BoostConfiguration boostConfiguration, DatabaseType databaseType, ApplicationSettings ApplicationSettings)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                    .CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<IRepository>();
        switch (databaseType)
        {
            case DatabaseType.SQLite:
                var dbpath = GetSqliteDbPath(environment, ApplicationSettings);
                if (boostConfiguration.ResetDbOnStartup)
                {
                    ResetSqliteDb(environment, dbpath);
                }

                if (ApplicationSettings.EnableMigrations.HasValue && ApplicationSettings.EnableMigrations.Value)
                {
                    RunSqliteDbMigrations(dbContext);
                }

                break;
            case DatabaseType.SQLServer:
                if (boostConfiguration.ResetDbOnStartup)
                {
                    ResetSqlServerDb(dbContext);
                }

                if (ApplicationSettings.EnableMigrations.HasValue && ApplicationSettings.EnableMigrations.Value)
                {
                    // RunSqlServerDbMigrations(dbContext, environment);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null);
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

    // ReSharper disable once UnusedMember.Local
    private static void RunSqlServerDbMigrations(IRepository context, IHostEnvironment environment)
    {
        var contentRootPath = environment.ContentRootPath;
        if (string.IsNullOrEmpty(contentRootPath))
        {
            throw new InvalidOperationException($"{nameof(contentRootPath)} is required in {nameof(ApplicationSettings)} when {nameof(contentRootPath)} is {contentRootPath}. Error: 17744ee6-0f70-4513-9841-e24a41861df3");
        }

        var pathToSeeds = Path.Combine(contentRootPath, "\\App_Data\\sql\\seeds\\");

        context.Database.Migrate();

        var conn = context.Database.GetDbConnection(); // Get Database connection
        var initialConnectionState = conn.State;
        var directoryinfo = new DirectoryInfo(pathToSeeds);
        if (!directoryinfo.Exists)
        {
            directoryinfo.Create();
        }

        var rgFiles = directoryinfo.GetFiles("*.sql");
        foreach (var fi in rgFiles)
        {
            try
            {
                var sql = fi.OpenText()
                            .ReadToEnd();
                if (initialConnectionState != ConnectionState.Open)
                {
                    conn.Open(); // open connection if not already open
                }

                sql = sql.Replace("\nGO", string.Empty);

                try
                {
                    context.Database.ExecuteSqlRaw(sql);
                }
                catch (Exception e)
                {
                    //TODO store logs in database
                    Console.WriteLine(e.Message);
                }
            }
            finally
            {
                if (initialConnectionState != ConnectionState.Open)
                {
                    conn.Close(); // only close connection if not initially open
                }
            }
        }
    }
}