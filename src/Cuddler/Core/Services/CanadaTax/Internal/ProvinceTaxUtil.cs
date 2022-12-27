namespace Cuddler.Core.Services.CanadaTax.Internal;

internal static class ProvinceTaxUtil
{
    public static TaxProvinceModel ByProvinceName(string? provinceName)
    {
        if (!string.IsNullOrEmpty(provinceName))
        {
            var code = provinceName.ToLower();
            if (code.Contains("alb"))
            {
                return GetProvinceUsingCode("AB");
            }

            if (code.Contains("bri"))
            {
                return GetProvinceUsingCode("BC");
            }

            if (code.Contains("man"))
            {
                return GetProvinceUsingCode("MB");
            }

            if (code.Contains("brun"))
            {
                return GetProvinceUsingCode("NB");
            }

            if (code.Contains("lab"))
            {
                return GetProvinceUsingCode("NL");
            }

            if (code.Contains("nor"))
            {
                return GetProvinceUsingCode("NT");
            }

            if (code.Contains("nov"))
            {
                return GetProvinceUsingCode("NS");
            }

            if (code.Contains("nun"))
            {
                return GetProvinceUsingCode("NU");
            }

            if (code.Contains("ont"))
            {
                return GetProvinceUsingCode("ON");
            }

            if (code.Contains("pri") || code.Contains("pei"))
            {
                return GetProvinceUsingCode("PE");
            }

            if (code.Contains("qu"))
            {
                return GetProvinceUsingCode("QC");
            }

            if (code.Contains("sas"))
            {
                return GetProvinceUsingCode("SK");
            }

            if (code.Contains("yuk"))
            {
                return GetProvinceUsingCode("YT");
            }
        }

        return GetProvinceUsingCode("AB");
    }

    public static decimal CalculateTax(decimal amount, decimal taxRate)
    {
        return Math.Round(amount * taxRate, 3, MidpointRounding.ToEven);
    }

    public static TaxResultModel CalculateTax(string provinceCode, decimal amount, bool hasPst, bool hasGst)
    {
        if (string.IsNullOrEmpty(provinceCode))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(provinceCode));
        }

        var province = GetProvinceUsingCode(provinceCode);
        var totalTax = CalculateTax(amount, province.TotalTax(hasPst, hasGst));
        var pstTax = CalculateTax(amount, province.TotalTax(hasPst, false));
        var gstTax = CalculateTax(amount, province.TotalTax(false, hasGst));

        return new TaxResultModel
        {
            Amount = amount,
            TaxProvince = province,
            TotalTax = totalTax,
            Gst = gstTax,
            Pst = pstTax,
            HasGst = hasGst,
            HasPst = hasPst
        };
    }

    public static TaxProvinceModel GetProvinceUsingCode(string? provinceCode)
    {
        if (string.IsNullOrEmpty(provinceCode))
        {
            throw new InvalidOperationException("fb967e62-fe18-4a26-957b-e09d0111ffea");
        }

        provinceCode = provinceCode.Replace(".", string.Empty);

        var province = ProvinceUtil.ListProvinces()
                                   .FirstOrDefault(p => string.Equals(p.Code, provinceCode, StringComparison.InvariantCultureIgnoreCase));

        return province ?? ByProvinceName(provinceCode);
    }
}