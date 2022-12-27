namespace Cuddler.Core.Services.TimeZones;

public static class ETimeZoneUtil
{
    public static DateTime ConvertToTimeZone(DateTime utcDateTime, ETimeZone timezone)
    {
        var timeZone = GetTimeZoneInfo(timezone);
        var time = TimeZoneInfo.ConvertTime(utcDateTime, timeZone);

        return time;
    }

    public static TimeZoneInfo GetTimeZoneInfo(ETimeZone eTimeZone)
    {
        var id = ETimeZoneHelper.ToLongString(eTimeZone);

        return TimeZoneInfo.FindSystemTimeZoneById(id);
    }

    public static double GetTimeZoneOffset(ETimeZone timeZone)
    {
        return timeZone switch
        {
            ETimeZone.PacificStandardTime => -8,
            ETimeZone.MountainStandardTime => -7,
            ETimeZone.CanadaCentralStandardTime => -6,
            ETimeZone.EasternStandardTime => -5,
            ETimeZone.NewfoundlandStandardTime => -3.5,
            ETimeZone.AtlanticStandardTime => -4,
            _ => throw new ArgumentOutOfRangeException(nameof(timeZone), timeZone, null)
        };
    }
}