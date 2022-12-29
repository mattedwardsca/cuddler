// ReSharper disable UnusedMethodReturnValue.Global

using Cuddler.Web.Api;

namespace Cuddler.Web.Controllers.Query;

public class CuddlerAndOrFilter<T> where T : CuddlerBaseFilter
{
    private readonly T _filter;

    public CuddlerAndOrFilter(T filter)
    {
        _filter = filter;
    }

    public T And()
    {
        _filter._query += "~and~";

        return _filter;
    }

    public T Or()
    {
        _filter._query += "~or~";

        return _filter;
    }

    public T ToFilter()
    {
        return _filter;
    }
}