using System.Globalization;

namespace CuddlerDev.Utils;

public static class CurrencyUtil
{
    public static string ToCurrency(this decimal value)
    {
        return value.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
    }

    public static string ToCurrency(this decimal value, int decimals)
    {
        return value.ToString($"C:{decimals}", CultureInfo.CreateSpecificCulture("en-CA"));
    }

    public static string ToCurrency(this double value)
    {
        return value.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
    }
}