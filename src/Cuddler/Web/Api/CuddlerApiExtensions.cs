using System.Linq.Expressions;
using Cuddler.Ui;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Web.Api;

public class CuddlerApi
{
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

    public static Mock Mock = new();

}

public class Mock
{
    public static TModel Object<TModel>()
    {
        return Activator.CreateInstance<TModel>();
    }

    public string String => Object<string>();
    
    public bool Bool => Object<bool>();
    
    public int Int => Object<int>();
    
    public decimal Decimal => Object<decimal>();
    
    public double Double => Object<double>();
    
    public DateTime DateTime => Object<DateTime>();
}