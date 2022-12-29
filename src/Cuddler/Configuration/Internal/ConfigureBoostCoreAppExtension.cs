using System.Net;
using System.Net.Sockets;
using Cuddler.Configuration.Identity.Exceptions;
using Cuddler.Data.Entities;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Cuddler.Configuration.Internal;

internal static class ConfigureBoostCoreAppExtension
{
    public static void ConfigureBoostCoreApp(this IApplicationBuilder app, IWebHostEnvironment environment, BoostConfiguration boostConfiguration, ApplicationSettings applicationSettings, Action<IApplicationBuilder>? middlewareAction)
    {
        UseErrorHandling(app, applicationSettings);

        if (!environment.IsDevelopment())
        {
            app.UseHsts();
        }

        UpgradeDatabaseUtil.UpgradeAuthenticationDatabase(app, environment, boostConfiguration, applicationSettings);

        app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()
                                      ?.Value!);

        app.UseHttpsRedirection();

        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx => {
                var headers = ctx.Context.Response.GetTypedHeaders();
                headers.CacheControl = new CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromDays(365)
                };
            }
        });

        app.UseRouting();

        app.UseResponseCaching();

        // UseXForwardedHeaders(app);

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession(); // The order of middleware is important. Call UseSession after UseRouting and before UseEndpoints

        if (middlewareAction != null)
        {
            middlewareAction.Invoke(app);
        }
        else
        {
            // default middleware
            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/Api"), appBuilder => {
                app.UseMiddleware<DefaultMiddleware>();
            });
        }

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        });
    }

    private static void UseErrorHandling(IApplicationBuilder app, ApplicationSettings appSettings)
    {
        if (appSettings.ShowErrors.HasValue && appSettings.ShowErrors.Value)
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(errorApp => errorApp.Run(async context => {
                var title = "Service Unavailable";

                var resolutionInstructions = "Return to the page you came from and try again.";
                var responseStatusCode = 500;
                string errorMessage;

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;
                var errorDetails = exception?.GetType()
                                            .Name;

                switch (exception)
                {
                    case InvalidClaimException:
                        var signInManager = context.GetService<SignInManager<AccountEntity>>();
                        await signInManager.SignOutAsync();

                        foreach (var cookie in context.Request.Cookies.Keys)
                        {
                            context.Response.Cookies.Delete(cookie);
                        }

                        context.Response.Redirect("/Identity/Login");
                        return;

                    case UnauthorizedAccessException unauthorized:
                        responseStatusCode = 401;
                        errorDetails = "Unauthorized";
                        resolutionInstructions = "You do not have permissions to access this resource.";
                        errorMessage = unauthorized.Message;
                        title = "Access Denied";
                        break;

                    case FileNotFoundException notfoundException:
                        responseStatusCode = 404;
                        errorDetails = "Not Found";
                        resolutionInstructions = "The resource cannot be found. Return to the page you came from and try again.";
                        errorMessage = notfoundException.Message;

                        break;

                    // case ResultException resultException:
                    //     responseStatusCode = 500;
                    //     var sb = new StringBuilder();
                    //     foreach ((var key, var value) in resultException.Errors)
                    //     {
                    //         sb.AppendLine($"{key} : {value}<br/>");
                    //     }
                    //
                    //     errorMessage = sb.ToString();
                    //
                    //     break;

                    default:
                        errorMessage = exception?.Message ?? throw new Exception("Unknown exception", exception);

                        break;
                }

                var remoteIpAddress = context.Connection.RemoteIpAddress;
                var result = "";
                if (remoteIpAddress != null)
                {
                    // If we got an IPV6 address, then we need to ask the network for the IPV4 address
                    // This usually only happens when the browser is on the same machine as the server.
                    if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = Dns.GetHostEntry(remoteIpAddress)
                                             .AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);
                    }

                    result = remoteIpAddress.ToString();
                }

                static string FullPath(HttpRequest request)
                {
                    var fullPath = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), request.PathBase.ToUriComponent(), request.Path.ToUriComponent(), request.QueryString.ToUriComponent());

                    if (fullPath.EndsWith("/"))
                    {
                        fullPath = fullPath[..^1];
                    }

                    return fullPath.ToLower();
                }

                var ipAddress = result;
                var pageUrl = FullPath(context.Request);
                context.Response.StatusCode = responseStatusCode;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html lang=\"en\">\r\n");
                await context.Response.WriteAsync("<head>\r\n");
                await context.Response.WriteAsync("<style type=\"text/css\">body{margin:5% auto 0 auto;padding:0 18px}.P{margin:0 22%}.O{margin-top:20px}.N{margin-top:10px}.M{margin:10px 0 30px 0}.L{margin-bottom:60px}.K{font-size:25px;color:#F90}.J{font-size:14px}.I{font-size:20px}.H{font-size:18px}.G{font-size:16px}.F{width:230px;float:left}.E{margin-top:5px}.D{margin:8px 0 0 -20px}.C{color:#3CF;cursor:pointer}.B{color:#909090;margin-top:15px}.A{line-height:30px}.hide_me{display:none}.MY{font-size:0.8em;}</style>\r\n");
                await context.Response.WriteAsync("</head>\r\n");
                await context.Response.WriteAsync(Error500Template.Format(title, responseStatusCode, ipAddress, pageUrl, errorDetails, resolutionInstructions, errorMessage));
                await context.Response.WriteAsync("</html>\r\n");
                await context.Response.WriteAsync(new string(' ', 512));
            }));
        }
    }
}