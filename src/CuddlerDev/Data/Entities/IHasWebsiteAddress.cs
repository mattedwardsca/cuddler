namespace CuddlerDev.Data.Entities;

public interface IHasWebsiteAddress
{
    public string? WebsiteBillingEmail { get; set; }

    public string? WebsiteBillingPhone { get; set; }

    public string? WebsiteBillingPostalCode { get; set; }

    public string? WebsiteBillingProvince { get; set; }

    public string? WebsiteBillingStreet1 { get; set; }

    public string? WebsiteBillingStreet2 { get; set; }

    public string? WebsiteName { get; set; }

    public double WebsiteShippingLatitude { get; set; }

    public double WebsiteShippingLongitude { get; set; }

    public string? WebsiteShippingPostalCode { get; set; }

    public string? WebsiteShippingStreet1 { get; set; }

    public string? WebsiteShippingStreet2 { get; set; }
}