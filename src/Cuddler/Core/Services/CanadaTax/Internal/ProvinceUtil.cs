namespace Cuddler.Core.Services.CanadaTax.Internal;

internal static class ProvinceUtil
{
    public static TaxProvinceModel Alberta => new()
    {
        Code = "AB",
        IsHarmonized = false,
        HasPst = false,
        Name = "Alberta",
        ProvincialTax = 0m,
        TaxLabel = "GST"
    };

    public static TaxProvinceModel BritishColumbia => new()
    {
        Code = "BC",
        IsHarmonized = false,
        HasPst = true,
        Name = "British Columbia",
        ProvincialTax = 0.07m,
        TaxLabel = "PST"
    };

    public static TaxProvinceModel Manitoba => new()
    {
        Code = "MB",
        IsHarmonized = false,
        HasPst = true,
        Name = "Manitoba",
        ProvincialTax = 0.07m,
        TaxLabel = "PST"
    };

    public static TaxProvinceModel NewBrunswick => new()
    {
        Code = "NB",
        IsHarmonized = true,
        HasPst = true,
        Name = "New Brunswick",
        ProvincialTax = 0.10m,
        TaxLabel = "HST"
    };

    public static TaxProvinceModel NewfoundlandAndLabrador => new()
    {
        Code = "NL",
        IsHarmonized = true,
        HasPst = true,
        Name = "Newfoundland and Labrador",
        ProvincialTax = 0.10m,
        TaxLabel = "HST"
    };

    public static TaxProvinceModel NorthwestTerritories => new()
    {
        Code = "NT",
        IsHarmonized = false,
        HasPst = false,
        Name = "Northwest Territories",
        ProvincialTax = 0m,
        TaxLabel = "GST"
    };

    public static TaxProvinceModel NovaScotia => new()
    {
        Code = "NS",
        IsHarmonized = true,
        HasPst = true,
        Name = "Nova Scotia",
        ProvincialTax = 0.10m,
        TaxLabel = "HST"
    };

    public static TaxProvinceModel Nunavut => new()
    {
        Code = "NU",
        IsHarmonized = false,
        HasPst = false,
        Name = "Nunavut",
        ProvincialTax = 0m,
        TaxLabel = "GST"
    };

    public static TaxProvinceModel Ontario => new()
    {
        Code = "ON",
        IsHarmonized = true,
        HasPst = true,
        Name = "Ontario",
        ProvincialTax = 0.08m,
        TaxLabel = "HST"
    };

    public static TaxProvinceModel PrinceEdwardIsland => new()
    {
        Code = "PE",
        IsHarmonized = true,
        HasPst = true,
        Name = "Prince Edward Island",
        ProvincialTax = 0.10m,
        TaxLabel = "HST"
    };

    public static TaxProvinceModel Quebec => new()
    {
        Code = "QC",
        IsHarmonized = false,
        HasPst = true,
        Name = "Québec",
        ProvincialTax = 0.09975m,
        TaxLabel = "QST"
    };

    public static TaxProvinceModel Saskatchewan => new()
    {
        Code = "SK",
        IsHarmonized = false,
        HasPst = true,
        Name = "Saskatchewan",
        ProvincialTax = 0.06m,
        TaxLabel = "PST"
    };

    public static TaxProvinceModel Yukon => new()
    {
        Code = "YT",
        IsHarmonized = false,
        HasPst = false,
        Name = "Yukon",
        ProvincialTax = 0m,
        TaxLabel = "GST"
    };

    public static List<TaxProvinceModel> ListProvinces()
    {
        return new List<TaxProvinceModel>
        {
            Alberta,
            BritishColumbia,
            Manitoba,
            NewBrunswick,
            NewfoundlandAndLabrador,
            NorthwestTerritories,
            NovaScotia,
            Nunavut,
            Ontario,
            PrinceEdwardIsland,
            Quebec,
            Saskatchewan,
            Yukon
        };
    }

    public static SortedDictionary<string, string> SelectList()
    {
        var provinces = ListProvinces();

        var sortedDictionary = new SortedDictionary<string, string>();
        foreach (var taxProvinceModel in provinces)
        {
            sortedDictionary.Add(taxProvinceModel.Code!, taxProvinceModel.Name!);
        }

        return sortedDictionary;
    }
}