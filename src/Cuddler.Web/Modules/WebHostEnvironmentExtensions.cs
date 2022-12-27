using Microsoft.AspNetCore.Hosting;

namespace Cuddler.Web.Modules;

public static class WebHostEnvironmentExtensions
{
    public static string GetVersion(this IWebHostEnvironment env, Type type)
    {
        return GetVersion(type);
    }

    private static string GetVersion(Type type)
    {
        return type.Assembly.ManifestModule.ModuleVersionId.ToString("N");
    }
}