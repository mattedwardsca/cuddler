using System.Linq.Expressions;
using Cuddler.Data.Entities;
using Cuddler.Web.Forms;
using Cuddler.Web.Kendo;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cuddler.Web.Ui;

public class CuddlerGridFields<TModel> : GridCuddlerFields where TModel : class
{
    public CuddlerGridFields() : base(typeof(TModel))
    {
    }

    public Type ModelType => typeof(TModel);

    public CuddlerGridFields<TModel> Bind(IData obj)
    {
        Model = obj;

        return this;
    }

    public CuddlerGridFields<TModel> Schema(Action<GridPropertyFactory<TModel>> configurator)
    {
        configurator(new GridPropertyFactory<TModel>(Properties));

        return this;
    }

    public class GridInputPropertyWrapper
    {
        private readonly FormField _formFieldProperty;

        public GridInputPropertyWrapper(FormField formFieldProperty)
        {
            _formFieldProperty = formFieldProperty;
        }

        public GridInputPropertyWrapper Col(int zeroBasedIndex)
        {
            _formFieldProperty.Col = zeroBasedIndex;

            return this;
        }

        public GridInputPropertyWrapper ContextType(string contextType)
        {
            _formFieldProperty.ContextType = contextType;

            return this;
        }

        public GridInputPropertyWrapper DefaultValue(object? defaultValue)
        {
            _formFieldProperty.Value = defaultValue?.ToString();

            return this;
        }

        public GridInputPropertyWrapper Description(string description)
        {
            _formFieldProperty.Description = description;

            return this;
        }

        public GridInputPropertyWrapper GridTemplate(string template)
        {
            _formFieldProperty.GridTemplate = template;

            return this;
        }

        public GridInputPropertyWrapper GridTemplate(EGridTemplate template)
        {
            _formFieldProperty.GridTemplate = KendoGridExtensions.GetGridTemplate(_formFieldProperty.Name, template.ToString());

            return this;
        }

        public GridInputPropertyWrapper HeadingHtmlAttributes(object? className)
        {
            _formFieldProperty.HeaderHtmlAttributes = className;

            return this;
        }

        public GridInputPropertyWrapper HtmlAttributes(object o)
        {
            _formFieldProperty.HtmlAttributes = o;

            return this;
        }

        public GridInputPropertyWrapper Label(string label)
        {
            _formFieldProperty.Label = label;

            return this;
        }

        public GridInputPropertyWrapper ReadOnly(bool b)
        {
            _formFieldProperty.ReadOnly = b;

            return this;
        }

        public GridInputPropertyWrapper Row(int zeroBasedIndex)
        {
            _formFieldProperty.Row = zeroBasedIndex;

            return this;
        }

        public GridInputPropertyWrapper Width(int width)
        {
            _formFieldProperty.GridColumnWidth = width;

            return this;
        }
    }

    public class GridPropertyFactory<T> where T : class
    {
        private readonly List<FormField> _properties;

        public GridPropertyFactory(List<FormField> properties)
        {
            _properties = properties;
        }

        public GridInputPropertyWrapper Add(Expression<Func<T, object?>> property, int row = 0, int col = 0)
        {
            var key = GetKey(property);
            var type = typeof(T);
            var inputProperty = FormFieldUtil.GetFormField(type, key) ?? FormFieldUtil.GetDefaultFormField(key, null);

            inputProperty.Row = row;
            inputProperty.Col = col;
            _properties.Add(inputProperty);

            return new GridInputPropertyWrapper(inputProperty);
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
}