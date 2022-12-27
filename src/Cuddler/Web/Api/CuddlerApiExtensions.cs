using System.Linq.Expressions;
using Cuddler.Core.Apis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Web.Api;

public class CuddlerApi
{
    public static CuddlerUri<T> Uri<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func);
    }

    //public static CuddlerUri<T> Block<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IBlockController
    //{
    //    return new CuddlerUri<T>().Endpoint(func);
    //}

    public static TModel Mock<TModel>()
    {
        return Activator.CreateInstance<TModel>();
    }
}