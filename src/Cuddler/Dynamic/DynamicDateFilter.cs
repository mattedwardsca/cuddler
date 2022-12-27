namespace Cuddler.Dynamic;

public class DynamicDateFilter : DynamicBaseFilter
{
    private readonly DynamicAndOrFilter<DynamicDateFilter> _andOr;

    public DynamicDateFilter(string key) : base(key)
    {
        _andOr = new DynamicAndOrFilter<DynamicDateFilter>(this);
    }

    public DynamicAndOrFilter<DynamicDateFilter> IsEqualTo(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~eq~{date}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicDateFilter> IsGreaterThan(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~gt~{date}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicDateFilter> IsGreaterThanOrEqualTo(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~gte~{date}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicDateFilter> IsLessThan(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~lt~{date}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicDateFilter> IsLessThanOrEqualTo(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~lte~{date}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicDateFilter> IsNotEqualTo(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~neq~{date}";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicDateFilter> IsNotNull()
    {
        _query += $"{_key}~isnotnull~null";

        return _andOr;
    }

    public DynamicAndOrFilter<DynamicDateFilter> IsNull()
    {
        _query += $"{_key}~isnull~null";

        return _andOr;
    }

    private static string GetDate(DateTime dateTime)
    {
        return $"datetime'{dateTime.Year}-{dateTime.Month.ToString("00")}-{dateTime.Day.ToString("00")}T{dateTime.Hour.ToString("00")}-{dateTime.Minute.ToString("00")}-{dateTime.Second.ToString("00")}'";
    }
}