namespace Cuddler.Core.Models;

public interface IHasParent : IData
{
    string? ParentId { get; set; }
}