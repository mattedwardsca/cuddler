namespace Cuddler.Dynamic;

public abstract class DynamicBaseFilter
{
    protected readonly string _key;
    public string _query;

    protected DynamicBaseFilter(string key)
    {
        _key = key;
        _query = string.Empty;
    }

    public override string ToString()
    {
        return _query;
    }
}