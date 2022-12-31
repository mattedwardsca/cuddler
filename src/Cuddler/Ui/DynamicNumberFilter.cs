// ReSharper disable UnusedMethodReturnValue.Global

namespace Cuddler.Web.Blocks;

public class DynamicNumberFilter : DynamicBaseFilter
{
    private readonly DynamicAndOrFilter<DynamicNumberFilter> _andOr;

    public DynamicNumberFilter(string key) : base(key)
    {
        _andOr = new DynamicAndOrFilter<DynamicNumberFilter>(this);
    }

    public DynamicAndOrFilter<DynamicNumberFilter> GreaterThan(int i)
    {
        _query += $"{_key}~gt~{i}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicNumberFilter> IsEqualTo(int i)
    {
        _query += $"{_key}~eq~{i}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicNumberFilter> IsGreaterThanOrEqualTo(int i)
    {
        _query += $"{_key}~gte~{i}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicNumberFilter> IsLessThan(int i)
    {
        _query += $"{_key}~lt~{i}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicNumberFilter> IsLessThanOrEqualTo(int i)
    {
        _query += $"{_key}~lte~{i}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicNumberFilter> IsNotEqualTo(int i)
    {
        _query += $"{_key}~neq~{i}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicNumberFilter> IsNotNull()
    {
        _query += $"{_key}~isnotnull~null";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicNumberFilter> IsNull()
    {
        _query += $"{_key}~isnull~null";

        return _andOr;
    }
}