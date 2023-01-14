using CuddlerDev.Web.Settings;

namespace CuddlerDev.Configuration;

public interface ISettingsService
{
    void InitWebsiteSettings(WebsiteSettings obj);

    Task SaveValue(string key, string? value);
}