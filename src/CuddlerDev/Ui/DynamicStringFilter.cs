// ReSharper disable UnusedMethodReturnValue.Global

namespace CuddlerDev.Ui;

public class DynamicStringFilter : DynamicBaseFilter
{
    private readonly DynamicAndOrFilter<DynamicStringFilter> _andOr;

    public DynamicStringFilter(string key) : base(key)
    {
        _andOr = new DynamicAndOrFilter<DynamicStringFilter>(this);
    }

    public DynamicAndOrFilter<DynamicStringFilter> Contains(string val)
    {
        _query += $"{_key}~contains~'{val}'";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> DoesNotContain(string val)
    {
        _query += $"{_key}~doesnotcontain~'{val}'";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> EndsWith(string val)
    {
        _query += $"{_key}~endswith~'{val}'";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> IsEmpty()
    {
        _query += $"{_key}~isempty~''";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> IsEqualTo(string val)
    {
        _query += $"{_key}~eq~'{val}'";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> IsNotEmpty()
    {
        _query += $"{_key}~isnotempty~''";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> IsNotEqualTo(string val)
    {
        _query += $"{_key}~neq~'{val}'";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> IsNotNull()
    {
        _query += $"{_key}~isnotnull~null";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> IsNull()
    {
        _query += $"{_key}~isnull~null";
        return _andOr;
    }

    public DynamicAndOrFilter<DynamicStringFilter> StartsWith(string val)
    {
        _query += $"{_key}~startswith~'{val}'";
        return _andOr;
    }
}