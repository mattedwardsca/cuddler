namespace CuddlerDev.Modules;

public interface IModuleService
{
    List<string> GetAllowedPaths();

    IApp? GetSegmentApp();

    ICuddlerModule GetSegmentModule();
}