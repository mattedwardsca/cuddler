namespace Cuddler.Data.Entities;

public interface IHasBillingAddress
{
    string? BillingCity { get; set; }

    string? BillingContactName { get; set; }

    string? BillingPostalCode { get; set; }

    string? BillingProvinceCode { get; set; }

    string? BillingStreet1 { get; set; }

    string? BillingStreet2 { get; set; }

    string? BillingType { get; set; }
}