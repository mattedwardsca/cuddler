using Cuddler.Modules;

namespace Cuddler.Configuration;

public interface IAppService
{
    List<IApp> ListShowInBottom();

    List<IApp> ListShowInTop();

    List<IApp> ListShowInMiddle();
}