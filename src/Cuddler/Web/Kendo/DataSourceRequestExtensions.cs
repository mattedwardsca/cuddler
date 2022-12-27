using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;

namespace Cuddler.Web.Kendo;

public static class DataSourceRequestExtensions
{
    public static List<TData> Filter<TData>(this DataSourceRequest request, IQueryable<TData> data, string q)
    {
        request.Filters = FilterDescriptorFactory.Create(q);

        return data.ToDataSourceResult(request)
                   .Data.Cast<TData>()
                   .ToList();
    }
}