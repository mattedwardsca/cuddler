namespace Cuddler.Data.Entities;

public interface IWebsiteBillingAddress
{
    string? WebsiteBillingCity { get; set; }

    string? WebsiteBillingEmail { get; set; }

    string? WebsiteBillingPhone { get; set; }

    string? WebsiteBillingPostalCode { get; set; }

    string? WebsiteBillingProvince { get; set; }

    string? WebsiteBillingStreet1 { get; set; }

    string? WebsiteBillingStreet2 { get; set; }

    string? WebsiteName { get; set; }
}