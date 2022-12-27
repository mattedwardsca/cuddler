using Cuddler.Core.Services.Accounts;
using Cuddler.Web.Modules;

namespace Cuddler.Web.Configuration.Internal;

internal class ClientAppService : IClientAppService
{
    private readonly IBoostModule _module;

    public ClientAppService(IBoostModule module)
    {
        _module = module;
    }

    public List<IClientApp> ListApps()
    {
        return _module.MiddleApps.Where(w => !w.Hidden)
                      .ToList();
    }

    public List<IClientApp> ListShowInTop()
    {
        return _module.TopApps.ToList();
    }
}