using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Web.Helpers;

public static class EOrganizationSizeHelper
{
    public static EOrganizationSize Parse(string? sEnum)
    {
        if (string.IsNullOrEmpty(sEnum))
        {
            return EOrganizationSize.SMB;
        }

        try
        {
            var eUnitOfMeasure = (EOrganizationSize)Enum.Parse(typeof(EOrganizationSize), sEnum, true);

            return eUnitOfMeasure;
        }
        catch (Exception)
        {
            return EOrganizationSize.SMB;
        }
    }

    public static List<SelectListItem> SelectList()
    {
        return Enum.GetValues<EOrganizationSize>()
                   .OrderBy(o => o)
                   .Select(s => new SelectListItem(s.ToString(), s.ToString()))
                   .ToList();
    }
}