using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Utils;

public static class SelectListItemUtil
{
    public static bool IsTextValue(IEnumerable? list)
    {
        if (list == null)
        {
            return false;
        }

        var type = list.GetType();
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            var itemType = type.GetGenericArguments()[0];

            return itemType == typeof(SelectListItem);
        }

        return false;
    }
}