using Microsoft.AspNetCore.Http;

namespace Cuddler.Web.Modules;

public static class AdminModeExtensions
{
    public static bool IsAdminMode(this HttpContext httpContext)
    {
        var isAdminMode = httpContext.Request.Cookies.GetBool(BoostCookies.AdminModeCookieName);
        return isAdminMode;
    }
}