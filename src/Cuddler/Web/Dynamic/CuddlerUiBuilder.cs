using Cuddler.Forms;

namespace Cuddler.Web.Dynamic;

public static class CuddlerUiBuilder
{
    public static CuddlerFormFields<TData> Form<TData>() where TData : class
    {
        return new CuddlerFormFields<TData>();
    }

    public static CuddlerFormGridFields<TData> Grid<TData>() where TData : class
    {
        return new CuddlerFormGridFields<TData>();
    }
}