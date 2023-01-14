namespace CuddlerDev.Data.Entities;

public interface IHasName : IData
{
    string Name { get; set; }
}