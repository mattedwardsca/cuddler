using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CuddlerDev.Web.Controllers;

/// <summary>
///     Class HandleErrorsAttribute. This class cannot be inherited.
///     Implements the <see cref="ActionFilterAttribute" />
///     Implements the <see cref="IExceptionFilter" />
/// </summary>
/// <seealso cref="ActionFilterAttribute" />
/// <seealso cref="IExceptionFilter" />
[AttributeUsage(AttributeTargets.Method)]
public sealed class HandleErrorsAttribute : ActionFilterAttribute, IExceptionFilter
{
    /// <summary>
    ///     Called after an action has thrown an <see cref="T:System.Exception" />.
    /// </summary>
    /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
    [DebuggerStepThrough]
    public void OnException(ExceptionContext context)
    {
        var errors = RecurseExceptions(context.Exception, 0);
        var model = new { Errors = errors };

        context.Result = new JsonResult(model);
        context.HttpContext.Response.StatusCode = 500;
        context.ExceptionHandled = true;
    }

    /// <summary>
    ///     Recurses the exceptions.
    /// </summary>
    /// <param name="ex">The ex.</param>
    /// <param name="level">The level.</param>
    /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
    [DebuggerStepThrough]
    private static IDictionary<string, string> RecurseExceptions(Exception? ex, int level)
    {
        var errorDictionary = new Dictionary<string, string>();
        while (true)
        {
            if (ex == null)
            {
                break;
            }

            var type = ex.GetType();
            if (type == typeof(DbUpdateException))
            {
                ex = ex.InnerException;

                continue;
            }

            errorDictionary.Add(level.ToString(), $"{type}. {ex.Message}");
            if (ex.InnerException != null && level < 5)
            {
                ex = ex.InnerException;
                level += 1;

                continue;
            }

            break;
        }

        return errorDictionary;
    }
}