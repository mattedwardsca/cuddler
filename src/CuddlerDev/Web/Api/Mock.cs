namespace CuddlerDev.Web.Api;

public static class Mock
{
    public static string[] StringArray => Array.Empty<string>();

    public static string String => string.Empty;

    public static bool Bool => false;

    public static int Int => 0;

    public static decimal Decimal => 0;

    public static double Double => 0;

    public static DateTime DateTime => DateTime.MinValue;

    public static TModel Object<TModel>()
    {
        return Activator.CreateInstance<TModel>();
    }
}