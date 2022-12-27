namespace Cuddler.Data.Entities;

public interface IHasContext : IData
{
    string? ContextId { get; set; }

    string? ContextType { get; set; }
}