using System.Reflection;
using Cuddler.Data.Entities;
using Cuddler.Forms;

namespace Cuddler.Web.Blocks;

public class DynamicFields
{
    private readonly Type _t;
    protected readonly List<FormField> Properties;
    private bool _isBound;
    public IData? Model;

    protected DynamicFields(Type t)
    {
        _t = t;
        Properties = new List<FormField>();
    }

    public Type GetDynamicType()
    {
        return _t;
    }

    public List<FormField> GetPropertyList()
    {
        if (Model != null && !_isBound)
        {
            foreach (var inputProperty in Properties)
            {
                var inputPropertyValue = GetPropertyAsString(Model, inputProperty.Name);
                inputProperty.Value = inputPropertyValue ?? inputProperty.DefaultValue?.ToString();
            }

            _isBound = true;
        }

        return Properties.OrderBy(o => o.Row)
                         .ThenBy(o => o.Col)
                         .ToList();
    }

    private static string? GetPropertyAsString(object obj, string propertyName)
    {
        var result = obj.GetType()
                        .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        ?.GetMethod?.Invoke(obj, Array.Empty<object>());

        return result?.ToString();
    }
}