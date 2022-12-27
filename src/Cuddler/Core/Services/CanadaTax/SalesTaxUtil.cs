using Cuddler.Core.Services.CanadaTax.Internal;

namespace Cuddler.Core.Services.CanadaTax;

public static class SalesTaxUtil
{
    public const decimal GST = 0.05m;

    public static TaxResultModel CalculateTax(string provinceCode, decimal amount, bool includePst, bool includeGst)
    {
        if (string.IsNullOrEmpty(provinceCode))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(provinceCode));
        }

        return ProvinceTaxUtil.CalculateTax(provinceCode, amount, includePst, includeGst);
    }

    public static TaxProvinceModel GetProvinceUsingCode(string? provinceCode)
    {
        return ProvinceTaxUtil.GetProvinceUsingCode(provinceCode);
    }

    public static TaxProvinceModel GetTaxProvince(string? provinceCode)
    {
        return ProvinceTaxUtil.GetProvinceUsingCode(provinceCode);
    }

    public static List<TaxProvinceModel> ListTaxProvinces()
    {
        return ProvinceUtil.ListProvinces();
    }
}