namespace Cuddler.Modules;

public interface IModuleService
{
    List<string> GetAllowedPaths();

    // IApp? GetAppById(string appId);

    // IApp? GetModuleAppById(IBoostModule module, string appId);

    IApp? GetSegmentApp();

    ICuddlerModule GetSegmentModule();
}