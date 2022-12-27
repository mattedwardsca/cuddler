using System.Globalization;

namespace Cuddler.Core.Utils;

public static class CurrencyUtil
{
    public static string ToCurrency(this decimal value)
    {
        return value.ToString("C", CultureInfo.CreateSpecificCulture("en-CA"));
    }
}