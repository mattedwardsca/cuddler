using Cuddler.Ui;

namespace Cuddler.Web.Controllers.Query;

public class CuddlerDateFilter : CuddlerBaseFilter
{
    private readonly CuddlerAndOrFilter<CuddlerDateFilter> _andOr;

    public CuddlerDateFilter(string key) : base(key)
    {
        _andOr = new CuddlerAndOrFilter<CuddlerDateFilter>(this);
    }

    public CuddlerAndOrFilter<CuddlerDateFilter> IsEqualTo(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~eq~{date}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerDateFilter> IsGreaterThan(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~gt~{date}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerDateFilter> IsGreaterThanOrEqualTo(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~gte~{date}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerDateFilter> IsLessThan(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~lt~{date}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerDateFilter> IsLessThanOrEqualTo(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~lte~{date}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerDateFilter> IsNotEqualTo(DateTime dateTime)
    {
        var date = GetDate(dateTime);
        _query += $"{_key}~neq~{date}";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerDateFilter> IsNotNull()
    {
        _query += $"{_key}~isnotnull~null";

        return _andOr;
    }

    public CuddlerAndOrFilter<CuddlerDateFilter> IsNull()
    {
        _query += $"{_key}~isnull~null";

        return _andOr;
    }

    private static string GetDate(DateTime dateTime)
    {
        return $"datetime'{dateTime.Year}-{dateTime.Month.ToString("00")}-{dateTime.Day.ToString("00")}T{dateTime.Hour.ToString("00")}-{dateTime.Minute.ToString("00")}-{dateTime.Second.ToString("00")}'";
    }
}