using Cuddler.Web.Settings;

namespace Cuddler.Configuration;

public interface ISettingsService
{
    void InitWebsiteSettings(WebsiteSettings obj);

    Task SaveValue(string key, string? value);
}