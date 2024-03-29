﻿using Microsoft.AspNetCore.Hosting;

namespace CuddlerDev.Modules.Utils;

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