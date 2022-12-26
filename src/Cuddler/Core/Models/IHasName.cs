namespace Cuddler.Core.Models;

public interface IHasName : IData
{
    string Name { get; set; }
}