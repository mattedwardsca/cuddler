using Microsoft.AspNetCore.Http;

namespace Cuddler.Ui;

public static class DynamicForms
{
    public static DynamicFormWrapper<TData, TService> Builder<TData, TService>(this HttpContext httpContext) where TData : class where TService : class
    {
        return new DynamicFormWrapper<TData, TService>(httpContext);
    }
}