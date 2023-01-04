namespace Cuddler.Data.Entities;

public interface IBillingAddress
{
    string? BillingCity { get; set; }

    string? BillingEmail { get; set; }

    string? BillingPhone { get; set; }

    string? BillingPostalCode { get; set; }

    string? BillingProvince { get; set; }

    string? BillingStreet1 { get; set; }

    string? BillingStreet2 { get; set; }
}