// ReSharper disable UnusedMethodReturnValue.Global

using Cuddler.Web.Api;

namespace Cuddler.Web.Query;

public class CuddlerNumberFilter : CuddlerBaseFilter
{
    private readonly CuddlerAndOrFilter<CuddlerNumberFilter> _andOr;

    public CuddlerNumberFilter(string key) : base(key)
    {
        _andOr = new CuddlerAndOrFilter<CuddlerNumberFilter>(this);
    }

    public CuddlerAndOrFilter<CuddlerNumberFilter> GreaterThan(int i)
    {
        _query += $"{_key}~gt~{i}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerNumberFilter> IsEqualTo(int i)
    {
        _query += $"{_key}~eq~{i}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerNumberFilter> IsGreaterThanOrEqualTo(int i)
    {
        _query += $"{_key}~gte~{i}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerNumberFilter> IsLessThan(int i)
    {
        _query += $"{_key}~lt~{i}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerNumberFilter> IsLessThanOrEqualTo(int i)
    {
        _query += $"{_key}~lte~{i}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerNumberFilter> IsNotEqualTo(int i)
    {
        _query += $"{_key}~neq~{i}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerNumberFilter> IsNotNull()
    {
        _query += $"{_key}~isnotnull~null";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerNumberFilter> IsNull()
    {
        _query += $"{_key}~isnull~null";

        return _andOr;
    }
}