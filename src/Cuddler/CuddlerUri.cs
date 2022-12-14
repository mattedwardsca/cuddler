using System.Linq.Expressions;
using Cuddler.Shared.Utils;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc.Rendering;

public abstract class CuddlerUri
{
    public string? _endpointUrl;

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

    protected static string GetBaseApiUrl(Type apiType)
    {
        var controllerName = apiType.Name.Replace("Controller", string.Empty);

        return $"/{AssemblyScannerUtil.GetAttribute<RouteAttribute>(apiType)?.Template.Replace("[controller]", controllerName)}";
    }
}

public class CuddlerUri<T> : CuddlerUri
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

    public CuddlerUri<T> Endpoint(Expression<Func<T, Task<IActionResult>>> func)
    {
        var body = func.Body as MethodCallExpression;

        var api = GetBaseApiUrl(typeof(T));
        var methodName = body!.Method.Name;

        var keys = typeof(T).GetMethods()
                            .Single(w => w.Name == methodName)
                            .GetParameters()
                            .Select(s => s.Name!)
                            .ToList();

        var parameterString = string.Join('&', GetApiParameters(keys, body.Arguments));

        _endpointUrl = $"{api}/{methodName}?{parameterString}";

        return this;
    }
}