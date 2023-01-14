using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CuddlerDev.Utils;

public static class GetServiceHttpContextExtension
{
    public static T GetService<T>(this HttpContext context)
    {
        return context.RequestServices.GetService<T>() ?? throw new NullReferenceException($"{typeof(T).Name} cannot be found in DI");
    }
}