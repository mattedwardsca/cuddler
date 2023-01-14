using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CuddlerDev.Utils;

public static class ModelStateExtensions
{
    public static void AddErrors(this ModelStateDictionary modelState, IDictionary<string, string> errors)
    {
        foreach (var (key, value) in errors)
        {
            modelState.TryAddModelError(key, value);
        }
    }
}