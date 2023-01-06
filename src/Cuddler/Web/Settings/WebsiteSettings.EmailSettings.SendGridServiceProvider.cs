namespace Cuddler.Web.Settings;

public class SendGridServiceProvider
{
    public string? SendGridApiKey { get; set; }

    public string? SendGridFromEmail { get; set; }

    public string? SendGridFromName { get; set; }

    public string? SendGridProviderName { get; set; }
}