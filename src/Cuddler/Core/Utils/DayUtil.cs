namespace Cuddler.Core.Utils;

public static class DayUtil
{
    public static DateTime GetEndOfDay(DateTime date)
    {
        return new DateTime(date.Ticks - 1).AddDays(1);
    }

    public static DateTime GetStartOfDay(DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day);
    }
}