using Cuddler.Core.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Pages.Shared.Cuddler.Kanban;

public static class EStationTypeHelper
{
    public static IEnumerable<string> List()
    {
        var strings = Enum.GetNames(typeof(EStationType));
        foreach (var s in strings)
        {
            yield return StringUtil.SplitCamelCase(s);
        }
    }

    public static List<StationModel> ListStationTypes()
    {
        return new List<EStationType>
            {
                EStationType.LightBlue,
                EStationType.DarkBlue,
                EStationType.Green,
                EStationType.Gray,
                EStationType.Yellow
            }.Select(s => new StationModel(StringUtil.SplitCamelCase(s.ToString()), s))
             .ToList();
    }

    public static List<SelectListItem> ListTextValues()
    {
        return ListStationTypes()
               .Select(s => new SelectListItem(s.Name, s.StationType.ToString()))
               .ToList();
    }

    public static EStationType Parse(string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return EStationType.LightBlue;
        }

        var succeeded = Enum.TryParse(str, out EStationType myStatus);

        return succeeded
            ? myStatus
            : EStationType.LightBlue;
    }
}