using Cuddler.Data.Entities;
using Cuddler.Web.Forms;

namespace Cuddler.Web.Dynamic;

public class CuddlerFormFields<TModel> : CuddlerFields where TModel : class
{
    public CuddlerFormFields() : base(typeof(TModel))
    {
    }

    public Type ModelType => typeof(TModel);

    public CuddlerFormFields<TModel> Bind(IData? obj)
    {
        Model = obj;

        return this;
    }

    public CuddlerFormFields<TModel> Schema(Action<DynamicFormPropertyFactory<TModel>> configurator)
    {
        configurator(new DynamicFormPropertyFactory<TModel>(Properties));

        return this;
    }
}