namespace Cuddler.Core.Services.CanadaTax;

public class TaxProvinceModel
{
    public string? Code { get; set; }

    public decimal FederalTax { get; set; } = SalesTaxUtil.GST;

    public bool HasGst { get; set; } = true;

    public bool HasPst { get; set; }

    public bool IsHarmonized { get; set; }

    public string? Name { get; set; }

    public decimal ProvincialTax { get; set; }

    public string? TaxLabel { get; set; }

    public override string? ToString()
    {
        return Name;
    }

    public decimal TotalTax(bool includePst, bool includeGst)
    {
        var pst = includePst
            ? ProvincialTax
            : 0;
        var gst = includeGst
            ? FederalTax
            : 0m;

        return pst + gst;
    }
}