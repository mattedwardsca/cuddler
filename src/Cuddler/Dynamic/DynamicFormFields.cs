using Cuddler.Core.Models;

namespace Cuddler.Dynamic;

public class DynamicFormFields<TModel> : DynamicFields where TModel : class
{
    public DynamicFormFields() : base(typeof(TModel))
    {
    }

    public Type ModelType => typeof(TModel);

    public DynamicFormFields<TModel> Bind(IData? obj)
    {
        Model = obj;

        return this;
    }

    public DynamicFormFields<TModel> Schema(Action<DynamicFormPropertyFactory<TModel>> configurator)
    {
        configurator(new DynamicFormPropertyFactory<TModel>(Properties));

        return this;
    }
}