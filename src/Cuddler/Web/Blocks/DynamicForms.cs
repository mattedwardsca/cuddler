﻿using Cuddler.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Cuddler.Web.Blocks;

public static class DynamicForms
{
    public static DynamicFormWrapper<TData, TService> Builder<TData, TService>(this HttpContext httpContext) where TData : class, IData where TService : class, IService
    {
        return new DynamicFormWrapper<TData, TService>(httpContext);
    }
}