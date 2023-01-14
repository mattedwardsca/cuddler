using PluralizeService.Core;

namespace CuddlerDev.Data.Utils;

public static class PluralizeUtil
{
    public static string Singularize(string pluralWord)
    {
        if (pluralWord.EndsWith("ies"))
        {
            return pluralWord[..^3] + "y";
        }

        if (pluralWord.EndsWith("es"))
        {
            return pluralWord[..^1];
        }

        return PluralizationProvider.Singularize(pluralWord);
    }

    public static string Pluralize(string singleWord)
    {
        return PluralizationProvider.Pluralize(singleWord);
    }
}