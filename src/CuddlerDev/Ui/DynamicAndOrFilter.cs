// ReSharper disable UnusedMethodReturnValue.Global

namespace CuddlerDev.Ui;

public class DynamicAndOrFilter<T> where T : DynamicBaseFilter
{
    private readonly T _filter;

    public DynamicAndOrFilter(T filter)
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