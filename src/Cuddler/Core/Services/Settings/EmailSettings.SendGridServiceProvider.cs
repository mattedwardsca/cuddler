namespace Cuddler.Core.Services.Settings;

public class SendGridServiceProvider
{
    public string? SendGridApiKey { get; set; } = "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";

    public string? SendGridFromEmail { get; set; } = "support@cocooncs.com";

    public string? SendGridFromName { get; set; } = "cocooncs support";

    public string? SendGridProviderName { get; set; } = "cocooncs";
}