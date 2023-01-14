using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CuddlerDev.Web.Controllers;

/// <summary>
///     Class ValidateInputAttribute. This class cannot be inherited.
///     Implements the <see cref="ActionFilterAttribute" />
/// </summary>
/// <seealso cref="ActionFilterAttribute" />
[AttributeUsage(AttributeTargets.Method)]
public sealed class ValidateModelAttribute : ActionFilterAttribute
{
    /// <summary>
    ///     Called when [action executing].
    /// </summary>
    /// <param name="context">The context.</param>
    /// <inheritdoc />
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var modelState = context.ModelState;
            var contextActionArguments = context.ActionArguments;

            var result = ConvertModelStateErrorsToDictionary(modelState, contextActionArguments);

            if (result.Any())
            {
                context.Result = new JsonResult(new
                {
                    StatusCode = 400,
                    Errors = result
                });

                context.HttpContext.Response.StatusCode = 400;
            }
        }
    }

    /// <summary>
    ///     Grabs the ModelState errors and converts them into a Dictionary that can be returned to the browser
    /// </summary>
    /// <param name="modelState">State of the model.</param>
    /// <param name="actionArguments">The action arguments.</param>
    /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
    private static Dictionary<string, string> ConvertModelStateErrorsToDictionary(ModelStateDictionary modelState, IDictionary<string, object?> actionArguments)
    {
        var result = new Dictionary<string, string>();
        foreach (var key in modelState.Keys)
        {
            foreach (var (subKey, value) in actionArguments)
            {
                if (value != null)
                {
                    var modelType = value.GetType();
                    if (modelType != typeof(FormCollection))
                    {
                        var property = value.GetType()
                                            .GetProperty(key);
                        if (property != null)
                        {
                            var isRequired = IsDefined(property, typeof(RequiredAttribute));
                            if (isRequired)
                            {
                                var isApiIgnored = IsDefined(property, typeof(ValidateNeverAttribute));
                                if (isApiIgnored)
                                {
                                    var modelStateEntry = modelState[key];
                                    if (modelStateEntry != null)
                                    {
                                        modelStateEntry.ValidationState = ModelValidationState.Valid;
                                    }

                                    break;
                                }
                                else
                                {
                                    var modelStateEntry = modelState[key];
                                    if (modelStateEntry != null)
                                    {
                                        foreach (var error in modelStateEntry.Errors)
                                        {
                                            result.Add(key, error.ErrorMessage);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    result[subKey] = $"{subKey} cannot be null";
                }
            }
        }

        return result;
    }
}