using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cuddler.Shared.Attributes;

/// <summary>
///     Class ValidateOutputAttribute. This class cannot be inherited.
///     Implements the <see cref="ActionFilterAttribute" />
/// </summary>
/// <seealso cref="ActionFilterAttribute" />
//[AttributeUsage(AttributeTargets.Method)]
public sealed class WrapOutputAttribute : ActionFilterAttribute
{
    /// <summary>
    ///     Called when [action executed].
    /// </summary>
    /// <param name="context">The context.</param>
    /// <inheritdoc />
    [DebuggerStepThrough]
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.ModelState.ErrorCount > 0)
        {
            context.HttpContext.Response.StatusCode = 400;
        }

        try
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (context.Result == null)
            {
                context.HttpContext.Response.StatusCode = 500;

                var dictionary = new Dictionary<string, string>
                {
                    { "", "Result is null" }
                };
                context.Result = new JsonResult(new
                {
                    StatusCode = 500,
                    Errors = dictionary
                });
            }
            else
            {
                var memberInfo = context.Result.GetType();
                if (memberInfo == typeof(JsonResult))
                {
                    HandleJsonResult(context);
                }
                else
                {
                    HandleObjectResult(context);
                }
            }
        }
        catch (Exception)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Result == null)
            {
                context.HttpContext.Response.StatusCode = 500;
                var dictionary = new Dictionary<string, string>
                {
                    { "", "Result is null" }
                };
                context.Result = new JsonResult(new
                {
                    StatusCode = 500,
                    Errors = dictionary
                });
            }
            else
            {
                var errors = GetModelStateErrors(context.ModelState);
                context.HttpContext.Response.StatusCode = 500;

                context.Result = new JsonResult(new
                {
                    StatusCode = 500,
                    Errors = errors
                });
            }
        }
    }

    /// <summary>
    ///     Gets the model state errors.
    /// </summary>
    /// <param name="modelStateDictionary">The model state dictionary.</param>
    /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
    private static Dictionary<string, string> GetModelStateErrors(ModelStateDictionary modelStateDictionary)
    {
        var errorDictionary = new Dictionary<string, string>();
        var modelStateKeys = modelStateDictionary.Keys;
        foreach (var key in modelStateKeys)
        {
            var exists = modelStateDictionary.TryGetValue(key, out var value);
            string? errors = null;
            if (exists && value != null)
            {
                var modelError = value.Errors.Select(e => e.ErrorMessage);
                errors = string.Join(", ", modelError);
            }

            if (errors != null)
            {
                errorDictionary.Add(key, errors);
            }
        }

        return errorDictionary;
    }

    /// <summary>
    ///     Handles the json result.
    /// </summary>
    /// <param name="context">The context.</param>
    private static void HandleJsonResult(ActionExecutedContext context)
    {
        if (context.Result != null)
        {
            var result = (JsonResult)context.Result;
            var modelStateDictionary = context.ModelState;

            var hasErrors = context.ModelState.ErrorCount > 0;
            result.Value = new
            {
                Data = hasErrors
                    ? null
                    : result.Value,
                StatusCode = hasErrors
                    ? 400
                    : context.HttpContext.Response.StatusCode,
                Errors = hasErrors
                    ? GetModelStateErrors(modelStateDictionary)
                    : null
            };
        }

    }

    /// <summary>
    ///     Handles the object result.
    /// </summary>
    /// <param name="context">The context.</param>
    private static void HandleObjectResult(ActionExecutedContext context)
    {
        if (context.Result != null)
        {
            var result = (ObjectResult)context.Result;
            var modelStateDictionary = context.ModelState;

            var hasErrors = context.ModelState.ErrorCount > 0;
            result.Value = new
            {
                StatusCode = hasErrors
                    ? 400
                    : context.HttpContext.Response.StatusCode,
                Errors = hasErrors
                    ? GetModelStateErrors(modelStateDictionary)
                    : null
            };
        }

    }
}