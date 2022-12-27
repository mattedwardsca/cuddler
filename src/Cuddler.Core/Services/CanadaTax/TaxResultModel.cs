namespace Cuddler.Core.Services.CanadaTax;

public class TaxResultModel
{
    public decimal Amount { get; set; }

    public decimal Gst { get; set; }

    public bool HasGst { get; set; }

    public bool HasPst { get; set; }

    public decimal Pst { get; set; }

    public TaxProvinceModel TaxProvince { get; set; } = null!;

    public decimal TotalTax { get; set; }

    public override string ToString()
    {
        return $"{Amount}";
    }
}