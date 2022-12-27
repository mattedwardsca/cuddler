namespace Cuddler.Web.Configuration;

public class ApplicationSettings
{
    public string? ContentRootFolder { get; set; }

    public bool? EnableMigrations { get; set; }

    public bool? ShowErrors { get; set; }

    public string? Subdomain { get; set; }
}