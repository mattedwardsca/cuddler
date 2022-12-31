namespace Cuddler.Ui;

public class ClientHandlerDescriptor
{
    /// <summary>A Razor template delegate.</summary>
    public Func<object, object>? TemplateDelegate { get; set; }

    /// <summary>
    ///     The name of the JavaScript function which will be called as a handler.
    /// </summary>
    public string? HandlerName { get; set; }

    public bool HasValue()
    {
        return HasValue(HandlerName) || TemplateDelegate != null;
    }

    private static bool HasValue(string? value)
    {
        return !string.IsNullOrEmpty(value);
    }
}