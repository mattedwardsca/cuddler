using Cuddler.Forms;

namespace Cuddler.Web.Blocks;

public static class CuddlerBuilder
{
    public static CuddlerForm<TData> Form<TData>() where TData : class
    {
        return new CuddlerForm<TData>();
    }

    public static CuddlerGrid<TData> Grid<TData>() where TData : class
    {
        return new CuddlerGrid<TData>();
    }
}