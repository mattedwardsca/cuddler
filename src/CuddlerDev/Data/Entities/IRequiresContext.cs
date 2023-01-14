namespace CuddlerDev.Data.Entities;

public interface IRequiresContext : IData
{
    string ContextId { get; set; }

    string ContextType { get; set; }
}