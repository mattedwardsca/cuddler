using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Utils;

public static class ProjectStatusUtil
{
    public static IEnumerable<string> ListStatusOptionsArray()
    {
        var employees = new[]
        {
            "New Project",
            "Bidding",
            "Accepted",
            "Fulfilled"
        };

        return employees;
    }

    public static List<SelectListItem> ListStatusOptions()
    {
        return ListStatusOptionsArray()
               .Select(s => new SelectListItem
               {
                   Text = s,
                   Value = s
               })
               .ToList();
    }
}