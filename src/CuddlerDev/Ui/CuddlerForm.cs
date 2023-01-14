using CuddlerDev.Forms;

namespace CuddlerDev.Ui;

public class CuddlerForm<TModel> : CuddlerFormFields where TModel : class
{
    public CuddlerForm() : base(typeof(TModel))
    {
    }

    public Type ModelType => typeof(TModel);

    public CuddlerForm<TModel> Bind(object? obj)
    {
        Model = obj;

        return this;
    }

    public CuddlerForm<TModel> Schema(Action<DynamicFormPropertyFactory<TModel>> configurator)
    {
        configurator(new DynamicFormPropertyFactory<TModel>(Properties));

        return this;
    }
}