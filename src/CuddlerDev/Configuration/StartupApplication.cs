using CuddlerDev.Configuration.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace CuddlerDev.Configuration;

public static class StartupApplication
{
    public static AuthenticationDatabaseBuilder Boost(this WebApplicationBuilder builder, Action<BoostConfiguration>? action = null)
    {
        var boostApplicationBuilder = new CuddlerApplicationBuilder(builder, action);


        return boostApplicationBuilder.InitServices();
    }

    public static void ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment environment, BoostConfiguration boostConfiguration, ApplicationSettings ApplicationSettings, Action<IApplicationBuilder>? middlewareAction)
    {
        app.ConfigureBoostCoreApp(environment, boostConfiguration, ApplicationSettings, middlewareAction);
    }

    public static void ConfigureApp_NotificationsApp(this IApplicationBuilder app)
    {
        //app.AddHub();
    }

    public static void InitServices_NotificationsApp(WebApplicationBuilder builder)
    {
        // SignalR https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-5.0
        // builder.Services.AddSignalR();
    }
}