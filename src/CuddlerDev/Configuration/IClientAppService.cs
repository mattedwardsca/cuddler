using CuddlerDev.Modules;

namespace CuddlerDev.Configuration;

public interface IAppService
{
    List<IApp> ListShowInBottom();

    List<IApp> ListShowInTop();

    List<IApp> ListShowInMiddle();
}