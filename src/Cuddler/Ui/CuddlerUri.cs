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
        for (var index = 0; index < parameterInfos.Count; index++)
        {
            var parameterInfo = parameterInfos[index];
            if (parameterInfo is MemberExpression memberExpression)
            {
                var parameterName = memberExpression.Member.Name;

                var objectMember = Expression.Convert(memberExpression, typeof(object));
                var getterLambda = Expression.Lambda<Func<object?>?>(objectMember);
                var getter = getterLambda.Compile()
                                         ?.Invoke();

                if (getter != null)
                {
                    //var stringValue = getter.ToString()!;

                    if (getter is DateTime asDate)
                    {
                        // DateTime asDate;
                        // try
                        // {
                        //     asDate = DateTime.ParseExact(stringValue, "yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture);
                        // }
                        // catch (Exception e)
                        // {
                        //     Console.WriteLine(e);
                        //     throw;
                        // }

                        yield return $"{parameterName}={FormatDateUtil.FormatJsonDate(asDate)}";
                    }

                    yield return $"{parameterName}={getter}";
                }
            }

            if (parameterInfo is ConstantExpression constantExpression)
            {
                var parameterName = infos[index];
                var objectMember = Expression.Convert(constantExpression, typeof(object));
                var getterLambda = Expression.Lambda<Func<object?>?>(objectMember);
                var getter = getterLambda.Compile()
                                         ?.Invoke();
                if (getter != null)
                {
                    if (getter is DateTime dateTime)
                    {
                        yield return $"{parameterName}={FormatDateUtil.FormatJsonDate(dateTime)}";
                    }

                    yield return $"{parameterName}={getter}";
                }
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