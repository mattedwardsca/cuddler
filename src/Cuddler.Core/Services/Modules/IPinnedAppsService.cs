using Cuddler.Data.Entities;

namespace Cuddler.Core.Services.Modules;

public interface IPinnedAppsService
{
    SettingEntity CreatePin(string appId, string userId);

    List<string> ListPinnedAppIdsForUser(string? userId);

    void RemovePin(string appId, string userId);
}