using Cuddler.Core.Services.Modules.Models;

namespace Cuddler.Core.Services.Modules;

public interface IClientAppService
{
    List<IClientApp> ListShowInTop();

    List<IClientApp> ListApps();
}