namespace Cuddler.Core.Modules;

public interface IModuleService
{
    List<string> GetAllowedPaths();

    // IApp? GetAppById(string appId);

    // IApp? GetModuleAppById(IBoostModule module, string appId);

    IClientApp? GetSegmentApp();

    IBoostModule GetSegmentModule();
}