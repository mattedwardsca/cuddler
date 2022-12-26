﻿using Cuddler.Core.Models;
using Cuddler.Core.Modules;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Dynamic;

public static class DynamicForms
{
    public static DynamicFormWrapper<TData, TService> Builder<TData, TService>(this HttpContext httpContext) where TData : class, IData where TService : class, IService
    {
        return new DynamicFormWrapper<TData, TService>(httpContext);
    }
}