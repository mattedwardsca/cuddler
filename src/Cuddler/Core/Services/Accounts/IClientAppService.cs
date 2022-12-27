using Cuddler.Web.Modules;

namespace Cuddler.Core.Services.Accounts;

public interface IClientAppService
{
    List<IClientApp> ListShowInTop();

    List<IClientApp> ListApps();
}