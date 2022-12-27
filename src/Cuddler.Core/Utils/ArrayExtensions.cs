namespace Cuddler.Core.Utils;

public static class ArrayExtensions
{
    public static T[] ToSingleItemArray<T>(this T item)
    {
        return new[]
        {
            item
        };
    }
}