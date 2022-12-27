namespace Cuddler.Web.Ui;

public static class CuddlerUiBuilder
{
    public static CuddlerFormFields<TData> Form<TData>() where TData : class
    {
        return new CuddlerFormFields<TData>();
    }

    public static CuddlerGridFields<TData> Grid<TData>() where TData : class
    {
        return new CuddlerGridFields<TData>();
    }
}