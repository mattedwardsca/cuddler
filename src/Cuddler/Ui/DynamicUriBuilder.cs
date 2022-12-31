using System.Linq.Expressions;
using System.Reflection;
using Cuddler.Forms.Ui;
using Cuddler.Utils;
using Kendo.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Web.Blocks;

public class DynamicUriBuilder<TController> : DynamicUriController
{
    public static string GetParameterName<TParameter>(Expression<Func<TParameter>> parameterToCheck)
    {
        var memberExpression = parameterToCheck.Body as MemberExpression;

        var parameterName = memberExpression!.Member.Name;

        return parameterName;
    }

    public static TParameter GetParameterValue<TParameter>(Expression<Func<TParameter>> parameterToCheck)
    {
        var parameterValue = parameterToCheck.Compile()
                                             .Invoke();

        return parameterValue;
    }

    public string Endpoint(Expression<Func<TController, Task<IActionResult>>> func)
    {
        var body = func.Body as MethodCallExpression;

        var api = GetBaseApiUrl(typeof(TController));
        var methodName = body!.Method.Name;

        MethodInfo methodInfo;
        try
        {
            methodInfo = typeof(TController).GetMethods()
                                            .Single(w => w.Name == methodName);
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException($"Method '{methodName}' exists more than once in {typeof(TController).Name}");
        }

        var keys = methodInfo.GetParameters()
                             .Select(s => s.Name!)
                             .ToList();
        var parameterString = string.Join('&', GetApiParameters(keys, body.Arguments));
        if (string.IsNullOrEmpty(parameterString))
        {
            _endpointUrl = $"{api}/{methodName}";
        }
        else
        {
            _endpointUrl = $"{api}/{methodName}?{parameterString}";
        }

        return _endpointUrl;
    }

    public DynamicFormFields<T> Form<T>() where T : class, IApiController
    {
        throw new NotImplementedException();
    }
}

public abstract class DynamicUriController
{
    public string? _endpointUrl;

    public List<CuddlerBaseFilter> FilterList { get; } = new();

    public List<SortDescriptor> SortList { get; } = new();

    public override string ToString()
    {
        return _endpointUrl ?? "UNINITIALIZED-ENDPOINT";
    }

    protected static IEnumerable<string> GetApiParameters(IReadOnlyList<string> infos, IReadOnlyList<Expression> parameterInfos)
    {
        for (var index = 0; index < parameterInfos.Count; index++)
        {
            var parameterInfo = parameterInfos[index];
            if (parameterInfo is MemberExpression memberExpression)
            {
                var parameterName = memberExpression.Member.Name;

                var objectMember = Expression.Convert(memberExpression, typeof(object));
                var getterLambda = Expression.Lambda<Func<object>>(objectMember);
                var getter = getterLambda.Compile()
                                         .Invoke()
                                         .ToString();

                yield return $"{parameterName}={getter}";
            }

            if (parameterInfo is ConstantExpression constantExpression)
            {
                var parameterName = infos[index];
                var objectMember = Expression.Convert(constantExpression, typeof(object));
                var getterLambda = Expression.Lambda<Func<object>>(objectMember);
                var getter = getterLambda.Compile()
                                         .Invoke()
                                         .ToString();

                yield return $"{parameterName}={getter}";
            }

            //if (parameterInfo is PropertyExpression propertyExpression)
            //{
            //}

            //else
            //{
            //    throw new NotImplementedException(parameterInfo.GetType().FullName);
            //}
        }
    }

    protected static string GetBaseApiUrl(Type typeOfController)
    {
        var controllerName = typeOfController.Name.Replace("Controller", string.Empty);

        return $"/{AssemblyScannerUtil.GetAttribute<RouteAttribute>(typeOfController)?.Template.Replace("[controller]", controllerName)}";
    }
}