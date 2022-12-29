using Cuddler.Modules;

namespace Cuddler.Configuration;

public interface IAppService
{
    List<IApp> ListShowInTop();

    List<IApp> ListApps();
}