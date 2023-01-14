namespace CuddlerDev.Web.Settings;

public sealed class WebsiteSettings
{
    public AccountSettings AccountSettings { get; set; } = new();

    public BillingSettings BillingSettings { get; set; } = new();

    public DocsSetting DocsSetting { get; set; } = new();

    public EmailSettings EmailSettings { get; set; } = new();

    public EncryptionSettings EncryptionSettings { get; set; } = new();

    public IdentitySettings IdentitySettings { get; set; } = new();

    public TranslationSettings TranslationSettings { get; set; } = new();

    public GeneralSettings GeneralSettings { get; set; } = new();
}