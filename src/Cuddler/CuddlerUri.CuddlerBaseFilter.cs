namespace Cuddler;

public abstract class CuddlerBaseFilter
{
    protected readonly string _key;
    public string _query;

    protected CuddlerBaseFilter(string key)
    {
        _key = key;
        _query = string.Empty;
    }

    public override string ToString()
    {
        return _query;
    }
}