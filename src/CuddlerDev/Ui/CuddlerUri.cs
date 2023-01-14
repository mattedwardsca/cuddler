using System.Linq.Expressions;
using System.Reflection;
using CuddlerDev.Data.Utils;
using Kendo.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CuddlerDev.Ui;

public abstract class CuddlerUri
{
    public string? _endpointUrl;

    public List<SortDescriptor> SortList { get; } = new();

    public List<CuddlerBaseFilter> FilterList { get; } = new();

    public string Uri => _endpointUrl ?? "UNINITIALIZED-ENDPOINT";

    public override string ToString()
    {
        return _endpointUrl ?? "UNINITIALIZED-ENDPOINT";
    }

    protected static object? GetValue(Expression argument)
    {
        var reduced = argument.CanReduce
            ? argument.Reduce()
            : argument;

        var o = Expression.Lambda(reduced)
                          .Compile()
                          .DynamicInvoke();

        return o;
    }

    protected static IEnumerable<string> GetApiParameters(IReadOnlyList<string> infos, IReadOnlyList<Expression> parameterInfos)
    {
        var list = new List<string>();

        for (var index = 0; index < parameterInfos.Count; index++)
        {
            var parameterInfo = parameterInfos[index];
            var parameterName = infos[index];
            var item = GetItem(parameterInfo, parameterName);
            if (item != null)
            {
                list.Add(item);
            }
        }

        return list;
    }

    protected static string? GetItem(Expression parameterInfo, string parameterName)
    {
        var getter = GetGetter(parameterInfo);
        switch (parameterInfo)
        {
            case MemberExpression:
            case ConstantExpression:
            {
                return getter switch
                {
                    null => null,
                    DateTime dateTime => $"{parameterName}={FormatDateUtil.FormatJsonDate(dateTime)}",
                    _ => $"{parameterName}={getter}"
                };

            }

            case NewExpression:
            case MethodCallExpression:
            case BinaryExpression:
            case UnaryExpression:
            case MemberInitExpression:
            {
                return null;
            }

            default:
            {
                throw new InvalidOperationException($"Invalid expression type '{parameterInfo.NodeType}'. Error: f2a8fe3c-2438-4526-8dad-7027523c2228.");
            }
        }
    }

    private static object? GetGetter(Expression parameterInfo)
    {

        var objectMember = Expression.Convert(parameterInfo, typeof(object));
        var getterLambda = Expression.Lambda<Func<object?>?>(objectMember);
        var getter = getterLambda.Compile()
                                 ?.Invoke();
        return getter;
    }

    protected static string GetBaseApiUrl(Type typeOfController)
    {
        var controllerName = typeOfController.Name.Replace("Controller", string.Empty);

        return $"/{GetAttribute<RouteAttribute>(typeOfController)?.Template.Replace("[controller]", controllerName)}";
    }

    private static TType? GetAttribute<TType>(Type t) where TType : Attribute
    {
        if (t.IsDefined(typeof(TType), false))
        {
            return (TType)Attribute.GetCustomAttribute(t, typeof(TType))!;
        }

        return null;
    }
}

public class CuddlerUri<TController> : CuddlerUri
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

    public CuddlerFormModel ToFormModel()
    {
        return new CuddlerFormModel
        {
            FormAction = this
        };
    }

    public CuddlerUri<TController> Endpoint(Expression<Func<TController, Task<IActionResult>>> func)
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

        var apiParameters = new List<string>();
        var methodParameters = methodInfo.GetParameters();


        for (var index = 0; index < body.Arguments.Count; index++)
        {
            var expression = body.Arguments[index];
            var type = expression.Type;

            switch (expression)
            {
                case MethodCallExpression methodCallExpression:
                {
                    var isMock = methodCallExpression.Method.DeclaringType?.Name == "Mock";

                    if (isMock)
                    {
                        continue;
                    }

                    break;
                }
                case MemberExpression memberExpression:
                {
                    var isMock = memberExpression.Member.DeclaringType?.Name == "Mock";

                    if (isMock)
                    {
                        continue;
                    }

                    break;
                }
            }

            var param = methodParameters[index].Name!;
            var item = GetItem(expression, param);
            if (item != null)
            {
                apiParameters.Add(item);
            }
        }

        var parameterString = string.Join('&', apiParameters);
        _endpointUrl = string.IsNullOrEmpty(parameterString)
            ? $"{api}/{methodName}"
            : $"{api}/{methodName}?{parameterString}";

        return this;
    }
}