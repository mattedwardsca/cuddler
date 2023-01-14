using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CuddlerDev.Data.Repository;

public static class SharedValueComparers
{
    public static ValueComparer<string[]> StringArray()
    {
        var valueComparer = new ValueComparer<string[]>((c1, c2) => c1!.SequenceEqual(c2!), c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => c.ToArray());

        return valueComparer;
    }
}