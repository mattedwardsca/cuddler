using System.Linq.Expressions;
using Cuddler.Ui;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Web.Api;

public class Cuddler
{
    public static CuddlerUri<T> Grid<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func);
    }

    public static CuddlerUri<T> Api<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func);
    }

    public static CuddlerFormModel Form<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func)
                                  .ToFormModel();
    }
}