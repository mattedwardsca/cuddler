namespace Cuddler.Data.Entities;

public interface IHasName : IData
{
    string Name { get; set; }
}