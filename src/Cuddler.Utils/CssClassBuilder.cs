namespace Cuddler.Utils;

public class CssClassBuilder
{
    private readonly List<string> _sb = new();

    public static CssClassBuilder CreateInstance()
    {
        return new CssClassBuilder();
    }

    public CssClassBuilder AddClass(string className)
    {
        _sb.Add(className);

        return this;
    }

    public override string ToString()
    {
        return string.Join(" ", _sb);
    }
}