using Cuddler.Core.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Dynamic;

public class DynamicFormInputPropertyWrapper
{
    private readonly FormField _formFieldProperty;

    public DynamicFormInputPropertyWrapper(FormField formFieldProperty)
    {
        _formFieldProperty = formFieldProperty;
    }

    public DynamicFormInputPropertyWrapper BindData(IEnumerable<SelectListItem> bindData)
    {
        _formFieldProperty.BindData = bindData;

        return this;
    }

    public DynamicFormInputPropertyWrapper Col(int zeroBasedIndex, int? length = null)
    {
        _formFieldProperty.Col = zeroBasedIndex;
        _formFieldProperty.ColLength = length;

        return this;
    }

    public DynamicFormInputPropertyWrapper ContextType(string contextType)
    {
        _formFieldProperty.ContextType = contextType;

        return this;
    }

    public DynamicFormInputPropertyWrapper DefaultValue(object? defaultValue)
    {
        _formFieldProperty.Value = defaultValue?.ToString();

        return this;
    }

    public DynamicFormInputPropertyWrapper Description(string description)
    {
        _formFieldProperty.Description = description;

        return this;
    }

    public DynamicFormInputPropertyWrapper HeadingHtmlAttributes(object? className)
    {
        _formFieldProperty.HeaderHtmlAttributes = className;

        return this;
    }

    public DynamicFormInputPropertyWrapper HideLabel()
    {
        _formFieldProperty.HideLabel = true;

        return this;
    }

    public DynamicFormInputPropertyWrapper HtmlAttributes(object o)
    {
        _formFieldProperty.HtmlAttributes = o;

        return this;
    }

    public DynamicFormInputPropertyWrapper Label(string label)
    {
        _formFieldProperty.Label = label;

        return this;
    }

    public DynamicFormInputPropertyWrapper CascadeFrom(string cascadeFrom)
    {
        _formFieldProperty.CascadeFrom = cascadeFrom;

        return this;
    }

    public DynamicFormInputPropertyWrapper ReadOnly(bool b)
    {
        _formFieldProperty.ReadOnly = b;

        return this;
    }

    public DynamicFormInputPropertyWrapper Required(bool isRequired = true)
    {
        _formFieldProperty.Required = isRequired;

        return this;
    }

    public DynamicFormInputPropertyWrapper Row(int zeroBasedIndex)
    {
        _formFieldProperty.Row = zeroBasedIndex;

        return this;
    }

    public DynamicFormInputPropertyWrapper Template(string inputTemplate)
    {
        _formFieldProperty.Field = inputTemplate;

        return this;
    }

    public DynamicFormInputPropertyWrapper Template(EFormField inputTemplate)
    {
        _formFieldProperty.Field = inputTemplate.ToString();

        return this;
    }

    public DynamicFormInputPropertyWrapper BindId(string? id)
    {
        _formFieldProperty.BindId = id;

        return this;
    }
}
