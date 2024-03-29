﻿using System.Security.Claims;
using CuddlerDev.Configuration.Identity.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CuddlerDev.Web.Controllers;

[AttributeUsage(AttributeTargets.All)]
public sealed class MustBeAdminToAccessApiAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var loggedInUserId = IsUserLoggedIn(context.HttpContext);
        if (!loggedInUserId)
        {
            RedirectToLoginPage(context);

            return;
        }

        var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)
                            ?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            RedirectToAccessDeniedPage(context);

            return;
        }

        var user = context.HttpContext.GetLoggedInAccount();

        if (!user.IsAdmin)
        {
            RedirectToAccessDeniedPage(context);
        }
    }

    private static string CombinedPath(HttpRequest request)
    {
        var combinedPath = $"{request.PathBase}{request.Path}";
        if (combinedPath.EndsWith("/"))
        {
            combinedPath = combinedPath[..^1];
        }

        return combinedPath;
    }

    private static bool IsUserLoggedIn(HttpContext httpContext)
    {
        return !string.IsNullOrEmpty(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)
                                                ?.Value);
    }

    private static void RedirectToAccessDeniedPage(ActionExecutingContext context)
    {
        context.Result = new RedirectResult($"/Identity/AccessDenied?returnUrl={CombinedPath(context.HttpContext.Request)}");
    }

    private static void RedirectToLoginPage(ActionExecutingContext context)
    {
        context.Result = new RedirectResult($"/Identity/Login?returnUrl={CombinedPath(context.HttpContext.Request)}");
    }
}