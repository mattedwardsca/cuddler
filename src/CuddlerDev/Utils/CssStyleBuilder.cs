namespace CuddlerDev.Utils;

public class CssStyleBuilder
{
    private readonly List<string> _sb = new();

    public static CssStyleBuilder CreateInstance()
    {
        return new CssStyleBuilder();
    }

    public CssStyleBuilder AddStyle(string key, object? value)
    {
        _sb.Add($"{key}:{value ?? string.Empty}");

        return this;
    }

    public override string ToString()
    {
        return string.Join(";", _sb);
    }
}