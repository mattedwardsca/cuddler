namespace CuddlerDev.Utils;

public static class SecondsUtil
{
    public static long ConvertHoursToSeconds(decimal timeInHours)
    {
        return (long)(timeInHours * 3600);
    }

    public static decimal ConvertSecondsToHours(decimal? timeInSeconds)
    {
        if (timeInSeconds == null)
        {
            return 0;
        }

        return (decimal)timeInSeconds / 3600;
    }

    public static decimal ConvertSecondsToHours(long? timeInSeconds)
    {
        if (timeInSeconds == null)
        {
            return 0;
        }

        return (decimal)timeInSeconds / 3600;
    }

    public static decimal ConvertSecondsToHours(double? timeInSeconds)
    {
        if (timeInSeconds == null)
        {
            return 0;
        }

        return (decimal)timeInSeconds / 3600;
    }

    public static decimal ConvertSecondsToHours(int? timeInSeconds)
    {
        if (timeInSeconds == null)
        {
            return 0;
        }

        return (decimal)timeInSeconds / 3600;
    }

    public static decimal ConvertSecondsToMinutes(int? timeInSeconds)
    {
        if (timeInSeconds == null)
        {
            return 0;
        }

        return (decimal)timeInSeconds / 60;
    }
}