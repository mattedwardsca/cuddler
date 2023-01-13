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
    public static string[] StringArray => Array.Empty<string>();

    public static string String => string.Empty;

    public static bool Bool => false;

    public static int Int => 0;

    public static decimal Decimal => 0;

    public static double Double => 0;

    public static DateTime DateTime => DateTime.MinValue;

    public static TModel Object<TModel>()
    {
        return Activator.CreateInstance<TModel>();
    }
}