using System.Linq.Expressions;
using Cuddler.Forms;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cuddler.Ui;

public class DynamicFormPropertyFactory<T> where T : class
{
    private readonly List<FormField> _formFields;

    public DynamicFormPropertyFactory(List<FormField> formFields)
    {
        _formFields = formFields;
    }

    public DynamicFormInputPropertyWrapper Add(Expression<Func<T, object?>> property, int row = 0, int col = 0)
    {
        var key = GetKey(property);
        var type = typeof(T);
        var inputProperty = FormFieldUtil.GetFormField(type, key) ?? FormFieldUtil.GetDefaultFormField(key, null);

        inputProperty.Row = row;
        inputProperty.Col = col;
        _formFields.Add(inputProperty);

        return new DynamicFormInputPropertyWrapper(inputProperty);
    }

    private static string GetKey<TType>(Expression<Func<TType, object?>> property)
    {
        var propertyBody = property.Body.Print();
        propertyBody = propertyBody.Replace("(object)", string.Empty);
        var firstPart = propertyBody.Split('.')
                                    .First();

        var key = propertyBody[(firstPart.Length + 1)..];

        return key;
    }
}