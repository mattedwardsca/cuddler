using CuddlerDev.Forms;
using CuddlerDev.Ui;

namespace CuddlerDev.Web.Blocks;

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