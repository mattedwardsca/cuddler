using Cuddler.Data.Utils;
using Cuddler.Web.Api;
using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;

namespace Cuddler.Web.Query;

public class CuddlerDbQuery
{
    public CuddlerDbQuery(Type elementType)
    {
        ElementType = elementType;
    }

    /// <summary>
    ///     Gets the type of the element(s) that are returned when the expression tree associated with this instance of
    ///     <see cref="T:System.Linq.IQueryable" /> is executed.
    /// </summary>
    /// <returns>
    ///     A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the
    ///     expression tree associated with this object is executed.
    /// </returns>
    public Type ElementType { get; }

    public List<CuddlerBaseFilter> FilterList { get; } = new();

    public List<SortDescriptor> SortList { get; } = new();
    //public string OverrideApiUrl(CuddlerUri apiUrl)
    //{
    //    return $"{apiUrl}?q={ToStringFilter()}";
    //}

    public string ToApiUrl(string endPoint)
    {
        var entityName = PluralizeUtil.Pluralize(ElementType.Name.Replace("Entity", string.Empty));

        return $"/A/p/i/s/Cuddler/{entityName}/{endPoint}?q={ToStringFilter()}";
    }

    public IList<IFilterDescriptor> ToFilterDescriptors()
    {
        return FilterDescriptorFactory.Create(ToStringFilter());
    }

    public string ToStringFilter()
    {
        if (FilterList.Count == 0)
        {
            return string.Empty;
        }

        if (FilterList.Count == 1)
        {
            return string.Join("", FilterList.Select(s => s.ToString()));
        }

        var stringList = new List<string>();
        foreach (var item in FilterList)
        {
            stringList.Add($"({item})");
        }

        return string.Join("~and~", stringList);
    }
}