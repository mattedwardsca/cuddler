namespace Cuddler.Web.Dynamic;

public class DynamicActionResult<TData> where TData : class
{
    public DynamicActionResult(TData? data)
    {
        Data = data;
        Errors = null;
        StatusCode = 200;
    }

    public DynamicActionResult(IDictionary<string, string> errors, int statusCode = 400)
    {
        Data = null;
        Errors = errors;
        StatusCode = statusCode;
    }

    public TData? Data { get; }

    public IDictionary<string, string>? Errors { get; }

    public int StatusCode { get; }

    public static DynamicActionResult<TData> Error400(string key, string value)
    {
        return new DynamicActionResult<TData>(new Dictionary<string, string> { { key, value } });
    }

    public static DynamicActionResult<TData> Error404(string key, string value)
    {
        return new DynamicActionResult<TData>(new Dictionary<string, string> { { key, value } }, 404);
    }
}