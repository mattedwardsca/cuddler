namespace Cuddler.Settings;

public sealed class GlobalSettings
{
    public BillingSettings BillingSettings { get; set; } = new();

    public EmailSettings EmailSettings { get; set; } = new();

    public EncryptionKeysSettings EncryptionKeysSettings { get; set; } = new();

    public IdentitySettings IdentitySettings { get; set; } = new();

    public TranslationSettings TranslationSettings { get; set; } = new();

    public WebsiteSettings WebsiteSettings { get; set; } = new();

    public bool EncryptFiles { get; set; }

    public AccountSettings AccountSettings { get; set; } = new();

    public void Reset(ISettingsService settingservice)
    {
        var resutl = settingservice.GetSettingsObject<GlobalSettings>();
        Reset(resutl);
    }

    public void Reset(GlobalSettings result)
    {
        EmailSettings = result.EmailSettings;
        WebsiteSettings = result.WebsiteSettings;
        TranslationSettings = result.TranslationSettings;
        IdentitySettings = result.IdentitySettings;
        EncryptionKeysSettings = result.EncryptionKeysSettings;
        BillingSettings = result.BillingSettings;
    }
}