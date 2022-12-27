namespace Cuddler.Core.Services.TimeZones;

public static class ETimeZoneHelper
{
    public static string[] List()
    {
        return Enum.GetNames(typeof(ETimeZoneHelper));
    }

    public static ETimeZone Parse(string? sEnum)
    {
        if (string.IsNullOrEmpty(sEnum))
        {
            return ETimeZone.MountainStandardTime;
        }

        var clean = sEnum.Replace(" ", "");

        return (ETimeZone)Enum.Parse(typeof(ETimeZone), clean, true);
    }

    public static string ToLongString(ETimeZone eEnum)
    {
        return eEnum switch
        {
            ETimeZone.PacificStandardTime => "Pacific Standard Time",
            ETimeZone.MountainStandardTime => "Mountain Standard Time",
            ETimeZone.CanadaCentralStandardTime => "Central Standard Time",
            ETimeZone.EasternStandardTime => "Eastern Standard Time",
            ETimeZone.NewfoundlandStandardTime => "Newfoundland Standard Time",
            ETimeZone.AtlanticStandardTime => "Atlantic Standard Time",
            _ => throw new ArgumentOutOfRangeException(nameof(eEnum))
        };
    }

    public static string ToLongString(string sEnum)
    {
        var parsed = Parse(sEnum);

        return ToLongString(parsed);
    }

    public static string ToShortString(string sEnum)
    {
        var parsed = Parse(sEnum);

        return ToShortString(parsed);
    }

    public static string ToShortString(ETimeZone eEnum)
    {
        return eEnum switch
        {
            ETimeZone.PacificStandardTime => "PST",
            ETimeZone.MountainStandardTime => "MST",
            ETimeZone.CanadaCentralStandardTime => "CST",
            ETimeZone.EasternStandardTime => "EST",
            ETimeZone.NewfoundlandStandardTime => "NST",
            ETimeZone.AtlanticStandardTime => "AST",
            _ => throw new ArgumentOutOfRangeException(nameof(eEnum))
        };
    }
}