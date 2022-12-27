namespace Cuddler.Dynamic;

public static class DictionaryExtensions
{
    public static bool GetBool(this Dictionary<string, string> dic, string key)
    {
        var exists = dic.TryGetValue(key, out var res);
        if (!exists || res == null)
        {
            return false;
        }

        try
        {
            return bool.Parse(res);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static decimal GetDecimal(this Dictionary<string, object> dic, string key)
    {
        var exists = dic.TryGetValue(key, out var res);
        if (exists)
        {
            return res == null
                ? 0
                : decimal.Parse(res.ToString() ?? string.Empty);
        }

        return 0;
    }

    public static double GetDouble(this Dictionary<string, string> dic, string key)
    {
        var exists = dic.TryGetValue(key, out var res);
        if (!exists || res == null)
        {
            return 0;
        }

        try
        {
            return double.Parse(res);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static double GetDouble(this IDictionary<string, object> dic, string key)
    {
        var exists = dic.TryGetValue(key, out var res);
        if (exists)
        {
            if (res == null)
            {
                return 0;
            }

            return double.Parse(res.ToString() ?? string.Empty);
        }

        return 0;
    }

    public static decimal GetValue(this Dictionary<string, decimal> dic, string key)
    {
        var value = dic.FirstOrDefault(x => x.Key == key);

        return value.Value;
    }

    public static string? GetValue(this Dictionary<string, string> dic, string key)
    {
        var exists = dic.TryGetValue(key, out var res);

        return exists
            ? res
            : null;
    }

    public static string? GetValue(this IDictionary<string, object?> dic, string key)
    {
        var exists = dic.TryGetValue(key, out var res);

        return exists
            ? res?.ToString()
            : null;
    }
}