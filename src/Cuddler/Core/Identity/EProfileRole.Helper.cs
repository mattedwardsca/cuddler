using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Core.Identity;

public static class EProfileRoleHelper
{
    public static string GetIdFromRole(EProfileRole role)
    {
        return List()
            .Single(w => w == role.ToString());
    }

    public static string GetIdFromRole(string role)
    {
        return List()
            .Single(w => w == role);
    }

    public static IEnumerable<string> List()
    {
        return new List<string>
        {
            nameof(EProfileRole.Coordinator),
            nameof(EProfileRole.Advisor),
            nameof(EProfileRole.Employee),
            nameof(EProfileRole.Auditor),
            nameof(EProfileRole.OrganizationAdmin)
        };
    }

    public static EProfileRole ParseRole(string sEnum)
    {
        return (EProfileRole)Enum.Parse(typeof(EProfileRole), sEnum, true);
    }

    public static List<SelectListItem> SelectList()
    {
        return List()
               .Select(s => new SelectListItem(s, s))
               .ToList();
    }
}