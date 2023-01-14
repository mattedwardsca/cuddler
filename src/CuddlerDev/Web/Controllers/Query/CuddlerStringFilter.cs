using CuddlerDev.Ui;

namespace CuddlerDev.Web.Controllers.Query;

public class CuddlerStringFilter : CuddlerBaseFilter
{
    private readonly CuddlerAndOrFilter<CuddlerStringFilter> _andOr;

    public CuddlerStringFilter(string key) : base(key)
    {
        _andOr = new CuddlerAndOrFilter<CuddlerStringFilter>(this);
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> Contains(string val)
    {
        _query += $"{_key}~contains~'{val}'";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> DoesNotContain(string val)
    {
        _query += $"{_key}~doesnotcontain~'{val}'";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> EndsWith(string val)
    {
        _query += $"{_key}~endswith~'{val}'";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> IsEmpty()
    {
        _query += $"{_key}~isempty~''";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> IsEqualTo(string val)
    {
        _query += $"{_key}~eq~'{val}'";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> IsNotEmpty()
    {
        _query += $"{_key}~isnotempty~''";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> IsNotEqualTo(string val)
    {
        _query += $"{_key}~neq~'{val}'";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> IsNotNull()
    {
        _query += $"{_key}~isnotnull~null";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> IsNull()
    {
        _query += $"{_key}~isnull~null";
        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerStringFilter> StartsWith(string val)
    {
        _query += $"{_key}~startswith~'{val}'";
        return _andOr;
    }
}