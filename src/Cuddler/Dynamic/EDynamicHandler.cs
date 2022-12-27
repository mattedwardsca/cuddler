namespace Cuddler.Dynamic;

public enum EDynamicHandler
{
    Restore,
    Update,
    Delete,
    Create,
    View
}

public static class EDynamicHandlerHelper
{
    public static EDynamicHandler Parse(string sEnum)
    {
        return (EDynamicHandler)Enum.Parse(typeof(EDynamicHandler), sEnum, true);
    }
}