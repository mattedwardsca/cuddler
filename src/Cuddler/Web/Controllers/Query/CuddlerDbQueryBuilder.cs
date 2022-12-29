namespace Cuddler.Web.Controllers.Query;

public static class CuddlerDbQueryBuilder
{
    public static CuddlerQueryBuilder<TData> Query<TData>() where TData : class
    {
        return new CuddlerQueryBuilder<TData>();
    }
}