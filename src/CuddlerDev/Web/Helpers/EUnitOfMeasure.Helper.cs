using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Web.Helpers;

public static class EUnitOfMeasureHelper
{
    public static EUnitOfMeasure Parse(string? sEnum)
    {
        if (string.IsNullOrEmpty(sEnum))
        {
            return EUnitOfMeasure.Each;
        }

        try
        {
            var eUnitOfMeasure = (EUnitOfMeasure)Enum.Parse(typeof(EUnitOfMeasure), sEnum, true);

            return eUnitOfMeasure;
        }
        catch (Exception)
        {
            return EUnitOfMeasure.Each;
        }
    }

    public static List<SelectListItem> SelectList()
    {
        return Enum.GetValues<EUnitOfMeasure>()
                   .OrderBy(o => o)
                   .Select(s => new SelectListItem(s.ToString(), s.ToString()))
                   .ToList();
    }

    public static string ToUnit(EUnitOfMeasure eOrderingEnum)
    {
        return eOrderingEnum switch
        {
            EUnitOfMeasure.Each => "ea.",
            EUnitOfMeasure.Bundle => "BDL",
            EUnitOfMeasure.Feet => "ft",
            EUnitOfMeasure.Litre => "L",
            EUnitOfMeasure.Pound => "lb",
            EUnitOfMeasure.Hour => "hr",
            EUnitOfMeasure.Day => "days",
            var _ => throw new ArgumentOutOfRangeException(nameof(eOrderingEnum), eOrderingEnum, null)
        };
    }

    public static string ToUnitPlural(EUnitOfMeasure eOrderingEnum)
    {
        return eOrderingEnum switch
        {
            EUnitOfMeasure.Each => "Quantity",
            EUnitOfMeasure.Feet => "Feet",
            EUnitOfMeasure.Bundle => "Items",
            EUnitOfMeasure.Litre => "Litres",
            EUnitOfMeasure.Pound => "Pounds",
            EUnitOfMeasure.Hour => "Hours",
            var _ => throw new ArgumentOutOfRangeException(nameof(eOrderingEnum), eOrderingEnum, null)
        };
    }
}