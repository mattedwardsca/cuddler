using System.Security.Claims;
using Cuddler.Core.Services.Identity.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cuddler.Core.Services.Identity.Attributes;

public class MustBeAdminToAccessPageAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
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

    private static void RedirectToAccessDeniedPage(ResultExecutingContext context)
    {
        context.Result = new RedirectResult($"/Identity/AccessDenied?returnUrl={CombinedPath(context.HttpContext.Request)}");
    }

    private static void RedirectToLoginPage(ResultExecutingContext context)
    {
        context.Result = new RedirectResult($"/Identity/Login?returnUrl={CombinedPath(context.HttpContext.Request)}");
    }
}