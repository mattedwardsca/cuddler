using System.Linq.Expressions;
using Cuddler.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler;

public static class CuddlerUriExtensions
{
    // ReSharper disable once UnusedParameter.Global
    public static CuddlerUri<T> Uri<T>(this Cuddler _, Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func);
    }

    // ReSharper disable once UnusedParameter.Global
    public static TModel Mock<TModel>(this Cuddler _)
    {
        return Activator.CreateInstance<TModel>();
    }
}