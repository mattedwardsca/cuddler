namespace CuddlerDev.Utils;

public static class TimeUtil
{
    public static string FormatHours(decimal number, string descriptor = "")
    {
        var qty = number == 1
            ? "hour"
            : "hours";

        return $"{number:0.0} {descriptor} {qty}";
    }

    public static string GetParts(int number)
    {
        return number == 1
            ? "part"
            : "parts";
    }
}