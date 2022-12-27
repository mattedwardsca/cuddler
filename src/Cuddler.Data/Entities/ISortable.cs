namespace Cuddler.Data.Entities;

public interface ISortable : IData
{
    int SortOrder { get; set; }
}