namespace CuddlerDev.Data.Entities;

public interface ISortable : IData
{
    int SortOrder { get; set; }
}