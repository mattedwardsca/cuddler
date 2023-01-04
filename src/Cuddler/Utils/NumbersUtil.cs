using System.Globalization;

namespace Cuddler.Utils;

public static class NumbersUtil
{
    public static int ToInt(object? obj, int defaultValue = 0)
    {
        if (obj == null)
        {
            return defaultValue;
        }

        var str = obj.ToString();
        if (string.IsNullOrEmpty(str))
        {
            return defaultValue;
        }

        try
        {
            return int.Parse(str);
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    public static string ToPercent(double value)
    {
        return value.ToString("P2", CultureInfo.CurrentCulture);
    }

    public static string ToPercent(decimal value)
    {
        return value.ToString("P2", CultureInfo.CurrentCulture);
    }

    public static string ToPercent(decimal? value)
    {
        return value == null
            ? 0.ToString("P2", CultureInfo.CurrentCulture)
            : ((decimal)value).ToString("P2", CultureInfo.CurrentCulture);
    }

    public static string ToPercent(double? value)
    {
        return value == null
            ? 0.ToString("P2", CultureInfo.CurrentCulture)
            : ((decimal)value).ToString("P2", CultureInfo.CurrentCulture);
    }

    public static string ToPercent(decimal value, int places)
    {
        return value.ToString($"P{places}", CultureInfo.CurrentCulture);
    }

    public static string ToPercent(decimal? value, int places)
    {
        return value == null
            ? 0.ToString($"P{places}", CultureInfo.CurrentCulture)
            : ((decimal)value).ToString($"P{places}", CultureInfo.CurrentCulture);
    }

    public static string ToPoints(decimal value)
    {
        return value.ToString("n0", CultureInfo.CurrentCulture) + " pts";
    }
}