using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cuddler.Web.Configuration.Internal;

internal static class ServicesConfigExtension
{
    public static void AddHstsSupport(this WebApplicationBuilder builder)
    {
        builder.Services.AddHsts(options => {
            options.Preload = true;
            options.IncludeSubDomains = true;
        });
    }

    public static void AddJsonSupportToApis(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers() // Adding this to avoid errors when JsonIgnore is missed or fails
               .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); // https://dotnetcoretutorials.com/2020/03/15/fixing-json-self-referencing-loop-exceptions/
    }

    public static void AddLoggingSupport(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(loggingBuilder => {
            loggingBuilder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            loggingBuilder.AddDebug();
        });
    }

    public static void AddRazorSupport(this WebApplicationBuilder builder)
    {
        // ConfigureApps(servicesSettings.AuthorizedAreas, builder);

        builder.WebHost.UseWebRoot("wwwroot");
        builder.WebHost.UseStaticWebAssets();

        builder.Services.AddRazorPages()
               .AddNewtonsoftJson(o => o.SerializerSettings.ContractResolver = new DefaultContractResolver())
               .AddViewLocalization()
               .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(type));

        if (builder.Environment.IsDevelopment())
        {
            //mvcBuilder.AddRazorRuntimeCompilation();

            //builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            //    {
            //        options.FileProviders.Add(new PhysicalFileProvider(Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "BoostDC.Web"))));
            //        // options.FileProviders.Add(new PhysicalFileProvider(Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "BoostDC.Apps.Billing"))));
            //    });

        }
    }
}