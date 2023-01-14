using System.Diagnostics;
using CuddlerDev.Configuration.Language;
using CuddlerDev.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CuddlerDev.Configuration.Internal;

internal static class AddRepositoryContextExtension
{
    [DebuggerStepThrough]
    public static void AddAuthenticationDatabase<TDbContext, TRepository>(this WebApplicationBuilder builder) where TDbContext : DbContext, TRepository, ITranslationRepository where TRepository : class
    {
        builder.Services.AddDbContext<TDbContext>(options => {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            if (builder.Environment.IsDevelopment())
            {
                options.LogTo(Console.WriteLine)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors();
            }
            else
            {
                options.LogTo(Console.WriteLine, LogLevel.None);
            }
        });

        builder.Services.AddScoped<TRepository>(p => p.GetRequiredService<TDbContext>());
        builder.Services.AddScoped<ITranslationRepository>(p => p.GetRequiredService<TDbContext>());
        builder.Services.AddScoped<DbContext>(p => p.GetRequiredService<TDbContext>());
        builder.Services.AddTransient<LanguageService>();
        builder.Services.AddScoped<ITranslationRepository>(p => p.GetRequiredService<TDbContext>());
    }

    [DebuggerStepThrough]
    public static void AddAdditionalDatabase<TDbContext>(this WebApplicationBuilder builder) where TDbContext : DbContext
    {
        builder.Services.AddDbContext<TDbContext>(options => {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            if (builder.Environment.IsDevelopment())
            {
                options.LogTo(Console.WriteLine)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors();
            }
            else
            {
                options.LogTo(Console.WriteLine, LogLevel.None);
            }
        });

        builder.Services.AddScoped(p => p.GetRequiredService<TDbContext>());
    }
}