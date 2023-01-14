using Microsoft.AspNetCore.Http;

namespace CuddlerDev.Modules.Utils;

public static class AdminModeExtensions
{
    public static bool IsAdminMode(this HttpContext httpContext)
    {
        var isAdminMode = httpContext.Request.Cookies.GetBool(CuddlerCookies.AdminModeCookieName);
        return isAdminMode;
    }
}