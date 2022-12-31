using System.Linq.Expressions;
using Cuddler.Forms.Ui;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Web.Api;

public class CuddlerApi
{
    public static CuddlerUri<T> Uri<T>(Expression<Func<T, Task<IActionResult>>> func) where T : class, IApiController
    {
        return new CuddlerUri<T>().Endpoint(func);
    }

    public static TModel Mock<TModel>()
    {
        return Activator.CreateInstance<TModel>();
    }
}