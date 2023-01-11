using System.Linq.Expressions;
using Cuddler.Ui;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Web.Api;

public class CuddlerApi
{
    public static Mock Mock = new();

    public static CuddlerUri<T> Grid<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func);
    }

    public static CuddlerUri<T> Uri<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func);
    }

    public static CuddlerFormModel Form<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func)
                                  .ToFormModel();
    }
}

public class Mock
{
    public static string String => Object<string>();

    public static bool Bool => Object<bool>();

    public static int Int => Object<int>();

    public static decimal Decimal => Object<decimal>();

    public static double Double => Object<double>();

    public static DateTime DateTime => Object<DateTime>();

    public static TModel Object<TModel>()
    {
        return Activator.CreateInstance<TModel>();
    }
}