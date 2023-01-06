namespace Cuddler.Utils;

public static class ListExtensions
{
    public static List<T> ToSingleItemList<T>(this T item)
    {
        return new List<T>
        {
            item
        };
    }
}