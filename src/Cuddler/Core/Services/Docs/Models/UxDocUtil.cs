using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Cuddler.Core.Utils;
using Cuddler.Web.Controllers;
using Cuddler.Web.Forms;
using Cuddler.Web.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Core.Services.Docs.Models;

public static class UxDocUtil
{
    public static List<ApiDocItem> GetApiDocItems()
    {
        var apiControllers = GetAllOfType<IApiController>()
            .ToList();

        var list = new List<ApiDocItem>();

        foreach (var apiType in apiControllers)
        {
            var apiDocItem = new ApiDocItem
            {
                Type = apiType,
                Name = apiType.Name.Replace("Controller", string.Empty)
                              .ToSplitCamelCase(),
                Id = WebIdUtil.GetGuidId(apiType.FullName),
                AssemblyName = apiType.Assembly.GetName()
                                      .Name,
                Description = ReflectionUtil.GetAttribute<DescriptionAttribute>(apiType)
                                            ?.Description,
                ApiUrl = GetBaseApiUrl(apiType),
                MustBeAdminToAccessApi = ReflectionUtil.GetAttribute<MustBeAdminToAccessApiAttribute>(apiType) != null,
                MustBeLoggedIn = ReflectionUtil.GetAttribute<AuthorizeAttribute>(apiType) != null
            };

            list.Add(apiDocItem);
        }

        return list.OrderBy(o => o.ApiUrl)
                   .ToList();
    }

    public static string GetBaseApiUrl(Type apiType)
    {
        var controllerName = apiType.Name.Replace("Controller", string.Empty);

        return $"/{AssemblyScannerUtil.GetAttribute<RouteAttribute>(apiType)?.Template.Replace("[controller]", controllerName)}";
    }

    public static List<UxDocItem> GetDocItems<T>() where T : class
    {
        var blockTypes = GetAllOfType<T>()
                         .OrderBy(o => o.FullName!.Split(".")
                                        .Last())
                         .ToList();

        var list = new List<UxDocItem>();

        foreach (var type in blockTypes)
        {
            var blockList = new UxDocItem
            {
                Type = type,
                Name = ToRazorTagName(type),
                TemplateName = ToPartialTagName(type),
                Id = WebIdUtil.GetGuidId(type.FullName),
                IsObsolete = Attribute.IsDefined(type, typeof(ObsoleteAttribute))
            };

            if (!blockList.IsObsolete)
            {
                list.Add(blockList);
            }
        }

        return list;
    }

    public static IEnumerable<UxDocItem> GetDocItems(List<Type> blockTypes)
    {
        foreach (var type in blockTypes)
        {
            var blockList = new UxDocItem
            {
                Type = type,
                Name = ToRazorTagName(type),
                AppName = ToAppName(type),
                Id = WebIdUtil.GetGuidId(type.FullName)
            };

            yield return blockList;
        }
    }

    public static void SetBlockProperties(UxDocItem uxDocItem)
    {
        var list = FormFieldUtil.ListFormFields(uxDocItem.Type)
                                .Where(w => w.Name != "Order")
                                .ToList();
        foreach (var InputProperty in list)
        {
            InputProperty.Name = ToRazorAttributeName(InputProperty.Name ?? throw new InvalidOperationException("3fbde4ec-da07-49c8-b7b3-dc2394d3cedb"));
        }

        uxDocItem.Properties = list;
    }

    private static List<Type> GetAllOfType<T>()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                        .Select(x => x)
                        .ToList();
    }

    private static string ToAppName(Type type)
    {
        return type.Assembly.FullName!.Split(',')[0]
                   .Split('.')
                   .Last()
                   .ToSplitCamelCase();
    }

    private static string ToPartialTagName(Type type)
    {
        return type.Name.Replace("TagHelper", "", StringComparison.InvariantCultureIgnoreCase);
    }

    private static string ToRazorAttributeName(string name)
    {
        return name.ToSplitCamelCase()
                   .Replace(" ", "-")
                   .ToLower();
    }

    private static string ToRazorTagName(Type type)
    {
        return type.Name.Replace("TagHelper", "", StringComparison.InvariantCultureIgnoreCase)
                   .ToSplitCamelCase()
                   .Replace(" ", "-")
                   .ToLower();
    }

    private static string ToSplitCamelCase(this string str)
    {
        return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
    }

    public static List<ApiDocItemEndpoints> GetApiEndpoints(ApiDocItem item)
    {
        var list = new List<ApiDocItemEndpoints>();

        var methods = item.Type.GetMethods();
        foreach (var methodInfo in methods)
        {
            var customAttributes = methodInfo.GetCustomAttributes(typeof(HttpPostAttribute), true);
            if (customAttributes.Any())
            {
                var httpPost = (HttpPostAttribute?)customAttributes[0];
                if (httpPost != null)
                {
                    var methodName = httpPost.Template;
                    if (string.IsNullOrEmpty(methodName))
                    {
                        throw new InvalidOperationException($"{methodInfo.DeclaringType!.Name} contains a POST method that is missing a named route.");
                    }


                    var apiDocItemEndpoints = GetApiEndpoint(item.ApiUrl!, methodName, methodInfo);
                    list.Add(apiDocItemEndpoints);
                }
            }
        }

        return list;
    }

    private static ApiDocItemEndpoints GetApiEndpoint(string baseUrl, string methodName, MethodInfo methodInfo)
    {
        if (string.IsNullOrEmpty(methodName))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(methodName));
        }

        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(baseUrl));
        }

        var apiDocItemEndpoints = new ApiDocItemEndpoints
        {
            Name = methodName.ToSplitCamelCase(),
            ApiUrl = $"{baseUrl}/{methodName}",
            Method = "POST",
            Parameters = GetApiParameters(methodInfo.GetParameters()),
            Description = GetMethodDescription(methodInfo)
        };
        return apiDocItemEndpoints;
    }

    private static string? GetMethodDescription(MethodInfo methodInfo)
    {

        var customAttributes = methodInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
        return customAttributes.Any()
            ? ((DescriptionAttribute?)customAttributes[0])?.Description
            : null;
    }

    private static IEnumerable<ApiParameter> GetApiParameters(ParameterInfo[] parameterInfos)
    {
        foreach (var parameterInfo in parameterInfos)
        {
            yield return new ApiParameter
            {
                ParameterType = parameterInfo.ParameterType.Name,
                Name = parameterInfo.Name!,
                Required = parameterInfo.GetCustomAttribute(typeof(RequiredAttribute), true) != null
            };
        }

    }
}