namespace Cuddler.Core.Services.TimeZones;

public class TimeZoneData
{
    public static List<TimeZoneInfo> ListTimeZones()
    {
        var timeZones = new List<TimeZoneInfo>
        {
            TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"),
            TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time"),
            TimeZoneInfo.FindSystemTimeZoneById("Canada Central Standard Time"),
            TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"),
            TimeZoneInfo.FindSystemTimeZoneById("Newfoundland Standard Time"),
            TimeZoneInfo.FindSystemTimeZoneById("Atlantic Standard Time")
        };

        return timeZones;
    }
}