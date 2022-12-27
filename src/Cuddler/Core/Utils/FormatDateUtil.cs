namespace Cuddler.Core.Utils;

public static class FormatDateUtil
{
    public static string DayMonthYear(DateTime dateTime)
    {
        return dateTime.ToString("dd MMM yyyy");
    }

    public static string DayMonthYear(DateTime? dateTime)
    {
        return dateTime == null
            ? ""
            : ((DateTime)dateTime).ToString("dd MMM yyyy");
    }

    /// <summary>
    ///     "{0:MM/dd/yyyy}"
    /// </summary>
    public static string? FormatCanadianDate(DateTime? date)
    {
        return date == null
            ? null
            : $"{date:MM/dd/yyyy}";
    }

    public static string? FormatCanadianDateTime(DateTime? date)
    {
        return date == null
            ? null
            : $"{date:MM/dd/yyyy h:mm tt}";
    }

    public static string? FormatCanadianMilitaryDateTime(DateTime? date)
    {
        return date == null
            ? null
            : $"{date:MM/dd/yyyy H:mm}";
    }

    /// <summary>
    ///     {date:dddd, MMMM d}
    /// </summary>
    public static string FormatDayOfWeekMonthDay(DateTime? date)
    {
        return $"{date:dddd, MMMM d}";
    }

    /// <summary>
    ///     {date:dddd, MMMM d, h:mm tt}
    /// </summary>
    public static string FormatDayOfWeekMonthYearDateTime(DateTime? date)
    {
        return $"{date:dddd, MMMM d, h:mm tt}";
    }

    public static string FormatDayTime(DateTime? date)
    {
        if (date == null)
        {
            return string.Empty;
        }

        return string.Format("{1:ddd} {0:h:mm tt}", date, date);
    }

    /// <summary>
    ///     #.##
    /// </summary>
    public static string FormatHours(decimal hours)
    {
        return hours.ToString("#.##");
    }

    /// <summary>
    ///     new Date({year}, {month}, {day}, {hour}, {min}, 0)
    /// </summary>
    public static string FormatJavascriptDate(DateTime? dateN)
    {
        if (dateN == null)
        {
            return string.Empty;
        }

        var date = (DateTime)dateN;
        var year = date.Year;
        var month = date.Month - 1;
        var day = date.Day;
        var hour = date.Hour;
        var min = date.Minute;

        return $"new Date({year}, {month}, {day}, {hour}, {min}, 0)";
    }

    public static string FormatJsonDate(DateTime? date)
    {
        return $"{date:yyyy-MM-dd}";
    }

    public static string FormatJsonDateTime(DateTime? date)
    {
        return $"{date:yyyy-MM-ddTH:mm:ss.fffK}";
    }

    public static string FormatMilitaryHoursToRegular(in int hours)
    {
        if (hours < 12)
        {
            return $"{hours} AM";
        }

        if (hours == 12)
        {
            return "12 PM";
        }

        var converted = hours - 12;

        return $"{converted} PM";
    }

    public static string FormatMinutes(int? timeSpent)
    {
        if (timeSpent == null)
        {
            return "0 min";
        }

        var seconds = (int)timeSpent;
        if (seconds == 60)
        {
            return "1 min";
        }

        if (seconds <= 3600)
        {
            var minutes = (float)seconds / 60;

            return minutes + " min";
        }

        var duration = TimeSpan.FromSeconds(seconds);
        if (duration.Minutes == 0)
        {
            return $"{duration.Hours} h";
        }

        return $"{duration.Hours}h {duration.Minutes}min";
    }

    public static string FormatMonthDay(DateTime? date)
    {
        return $"{date:MMM d}";
    }

    public static string FormatMonthDayTime(DateTime? date)
    {
        if (date == null)
        {
            return string.Empty;
        }

        return string.Format("{1:MMM d} at {0:h:mm tt}", date, date);
    }

    public static string? FormatMonthDayYear(DateTime? date)
    {
        if (date == null)
        {
            return null;
        }

        return $"{date:MMM d, yyyy}";
    }

    public static string FormatFormatYearMonthDayTime(DateTime? date)
    {
        if (date == null)
        {
            return string.Empty;
        }

        return string.Format("{1:yyyy-MM-dd} at {0:h:mm tt}", date, date);
    }

    public static string FormatYearMonthDay(DateTime date)
    {
        return $"{date:yyyy-MM-dd}";
    }

    public static string FormatYear(DateTime date)
    {
        return $"{date:yyyy}";
    }

    public static string? FormatYearMonthDay(DateTime? date)
    {
        if (date == null)
        {
            return null;
        }

        return $"{date:yyyy-MM-dd}";
    }

    public static string? FormatMonthYear(DateTime? date)
    {
        if (date == null)
        {
            return null;
        }

        return $"{date:MMM yyyy}";
    }

    public static string FormatRazorDate(in DateTime? date)
    {
        return $"{date:MM/dd/yyyy}";
    }

    public static string? FormatTime(DateTime? date)
    {
        return date == null
            ? null
            : $"{date:h:mm tt}";
    }

    public static string FormatTime(int? seconds)
    {
        var value = seconds ?? 0;
        var duration = TimeSpan.FromSeconds(value);
        var output = $"{duration.Hours:00}:{duration.Minutes:00}";

        return output;
    }

    public static long FormatTimeMillis(DateTime? date)
    {
        if (date.HasValue)
        {
            return date.Value.ToFileTimeUtc();
        }

        return 0;
    }

    public static string FormatTimespent(int? timeSpent)
    {
        var duration = timeSpent.HasValue == false
            ? new TimeSpan(0)
            : TimeSpan.FromSeconds(timeSpent.Value);

        return $"{duration.Hours:00}:{duration.Minutes:00}";
    }

    // TODO Test
    public static string ToQueryDate(DateTime dateTime)
    {
        return $"{dateTime.Month}/{dateTime.Day}/{dateTime.Year}";
    }
}