using System.Linq.Expressions;
using System.Reflection;
using Cuddler.Data.Utils;
using Cuddler.Forms;
using Kendo.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Ui;

public abstract class CuddlerUri
{
    public string? _endpointUrl;

    public List<SortDescriptor> SortList { get; } = new();

    public List<CuddlerBaseFilter> FilterList { get; } = new();

    public override string ToString()
    {
        return _endpointUrl ?? "UNINITIALIZED-ENDPOINT";
    }

    protected static object? GetValue(Expression argument)
    {
        var reduced = argument.CanReduce
            ? argument.Reduce()
            : argument;

        //var memberExpression = argument;

        //var parameterName = memberExpression.Member.Name;
        //var objectMember = Expression.Convert(memberExpression, typeof(object));
        //var getterLambda = Expression.Lambda<Func<object?>?>(objectMember);
        //var getter = GetGetter(getterLambda);

        //var getter = invoke?.ToString();

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

            switch (parameterInfo)
            {
                case MemberExpression memberExpression:
                {
                    var parameterName = memberExpression.Member.Name;
                    var getter = GetGetter(parameterInfo);
                    ListAdd(getter, list, parameterName);

                    break;
                }

                case ConstantExpression constantExpression:
                {
                    var parameterName = infos[index];
                    var getter = GetGetter(parameterInfo);
                    ListAdd(getter, list, parameterName);

                    break;
                }

                case NewExpression:
                case MethodCallExpression:
                {
                    // Don't add these
                    break;
                }

                default:
                {
                    // https://learn.microsoft.com/en-us/dotnet/api/system.linq.expressions.binaryexpression?view=net-7.0
                    throw new InvalidOperationException($"Invalid expression type. Error: f2a8fe3c-2438-4526-8dad-7027523c2228.");
                }
            }
        }

        return list;
    }

    private static object? GetGetter(Expression parameterInfo)
    {

        var objectMember = Expression.Convert(parameterInfo, typeof(object));
        var getterLambda = Expression.Lambda<Func<object?>?>(objectMember);
        var getter = getterLambda.Compile()
                                 ?.Invoke();
        return getter;
    }

    private static void ListAdd(object? getter, List<string> list, string parameterName)
    {

        if (getter != null)
        {
            if (getter is DateTime dateTime)
            {
                list.Add($"{parameterName}={FormatDateUtil.FormatJsonDate(dateTime)}");

                return;
            }

            list.Add($"{parameterName}={getter}");
        }
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
    public List<FormField> FormFields = new();

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
            FormFields = FormFields,
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

        var methodParameters = methodInfo.GetParameters();
        foreach (var argument in body.Arguments)
        {
            var type = argument.Type;

            if (type.IsClass && type != typeof(string))
            {
                var obj = GetValue(argument);
                FormFields.AddRange(FormFieldUtil.ListFormFields(type, obj));
            }
        }

        var keys = methodParameters.Select(s => s.Name!).ToList();
        var apiParameters = GetApiParameters(keys, body.Arguments);
        var parameterString = string.Join('&', apiParameters);
        if (string.IsNullOrEmpty(parameterString))
        {
            _endpointUrl = $"{api}/{methodName}";
        }
        else
        {
            _endpointUrl = $"{api}/{methodName}?{parameterString}";
        }

        return this;
    }

    // public CuddlerFormFields<T> Form<T>() where T : class, IApiController
    // {
    //     throw new NotImplementedException();
    // }
}