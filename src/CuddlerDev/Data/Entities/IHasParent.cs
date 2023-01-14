namespace CuddlerDev.Data.Entities;

public interface IHasParent : IData
{
    string? ParentId { get; set; }
}