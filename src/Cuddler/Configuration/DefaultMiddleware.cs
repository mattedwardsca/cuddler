using System.Security.Claims;
using Cuddler.Configuration.Identity.Exceptions;
using Cuddler.Configuration.Identity.Extensions;
using Cuddler.Data.Entities;
using Cuddler.Modules;
using Cuddler.Web.Utils;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Cuddler.Configuration;

public class DefaultMiddleware
{
    private readonly RequestDelegate _next;

    public DefaultMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public static bool FilterPaths(HttpContext context)
    {
        var env = context.GetService<IHostEnvironment>();
        var isDevelopment = env.IsDevelopment();

        var pathValue = context.Request.Path.Value;
        if (pathValue != null)
        {
            if (isDevelopment)
            {
                if (pathValue.StartsWith("/Install"))
                {
                    return false;
                }
            }

            var filterPaths = pathValue is not (null or "/") && !pathValue.StartsWith("/Identity") && !pathValue.StartsWith("/Terms") && !pathValue.StartsWith("/Api") && !pathValue.StartsWith("/static") && !pathValue.StartsWith("/_content") && !pathValue.StartsWith("/blocks") && !pathValue.StartsWith("/forms") && !pathValue.StartsWith("/favicon.ico");

            return filterPaths;
        }

        return false;
    }

    //[DebuggerStepThrough]
    public async Task InvokeAsync(HttpContext context)
    {
        var pathValue = context.Request.Path.Value;

        var moduleService = context.GetService<IModuleService>();

        if (IsPathMapped(moduleService, pathValue))
        {
            // MUST BE REGISTERED APP
            var app = moduleService.GetSegmentApp();
            if (app == null)
            {
                RedirectToAccessDeniedPage(context);
                return;
            }

            // REDIRECT USERS WHO ARE NOT LOGGED IN
            if (UserNotLoggedIn(context, out var user) || user == null)
            {
                return;
            }

            // REDIRECT USERS WHO HAVE NOT REGISTERED AN ORGANIZATION
            if (UserMissingOrganization(context, user))
            {
                return;
            }

            if (app.IsForClients)
            {
                await _next(context);

                return;
            }

            if (app.IsForAdmins)
            {
                if (user.IsAdmin)
                {
                    await _next(context);

                    return;
                }
            }
        }

        RedirectToAccessDeniedPage(context);
    }

    private static bool IsPathMapped(IModuleService moduleService, string? pathValue)
    {
        if (string.IsNullOrEmpty(pathValue))
        {
            return false;
        }

        var allowedPaths = moduleService.GetAllowedPaths();
        var isValidPath = allowedPaths.FirstOrDefault(w => pathValue.StartsWith(w, StringComparison.InvariantCultureIgnoreCase)) != null;

        return isValidPath;
    }

    private static string? LoggedInUserId(HttpContext httpContextAccessor)
    {
        if (httpContextAccessor.User.Identity is not { IsAuthenticated: true })
        {
            return null;
        }

        var userId = httpContextAccessor.User.FindFirst(ClaimTypes.NameIdentifier)
                                        ?.Value;

        return userId;
    }

    private static void RedirectOrganization(HttpContext httpContext)
    {
        httpContext.Response.Redirect($"/Identity/CreateOrganization?returnUrl={httpContext.Request.CombinedPath()}");
    }

    private static void RedirectToAccessDeniedPage(HttpContext httpContext)
    {
        httpContext.Response.Redirect($"/Identity/AccessDenied?returnUrl={httpContext.Request.CombinedPath()}");
    }

    private static void RedirectToLoginPage(HttpContext httpContext)
    {
        httpContext.Response.Redirect($"/Identity/Login?returnUrl={httpContext.Request.CombinedPath()}");
    }

    private static bool UserMissingOrganization(HttpContext context, AccountEntity user)
    {
        if (string.IsNullOrEmpty(user.Profile.OrganizationId))
        {
            RedirectOrganization(context);

            return true;
        }

        return false;
    }

    private static bool UserNotLoggedIn(HttpContext context, out AccountEntity? user)
    {
        user = null;

        var loggedInUserId = LoggedInUserId(context);
        if (string.IsNullOrEmpty(loggedInUserId))
        {
            RedirectToLoginPage(context);
            return true;
        }

        try
        {
            user = context.GetLoggedInAccount();
        }
        catch (InvalidClaimException)
        {
            foreach (var cookie in context.Request.Cookies.Keys)
            {
                context.Response.Cookies.Delete(cookie);
            }

            RedirectToAccessDeniedPage(context);
            return true;
        }

        return false;
    }
}