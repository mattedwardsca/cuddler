namespace Cuddler.Settings;

public class SmtpServiceProvider
{
    public string? SmtpPassword { get; set; }

    public int SmtpPort { get; set; }

    public string? SmtpServer { get; set; }

    public string? SmtpUser { get; set; }
}