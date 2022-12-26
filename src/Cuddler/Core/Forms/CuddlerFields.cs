using System.Reflection;
using Cuddler.Core.Models;

namespace Cuddler.Core.Forms;

public class CuddlerFields
{
    private readonly Type _t;
    protected readonly List<FormField> Properties;
    private bool _isBound;
    public IData? Model;

    protected CuddlerFields(Type t)
    {
        _t = t;
        Properties = new List<FormField>();
    }

    public Type GetCuddlerType()
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