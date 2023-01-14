using CuddlerDev.Core.Ecommerce;

namespace CuddlerDev.Core.Projects;

public interface IProjectType
{
    string Id { get; }

    string Name { get; }

    string OrderName { get; }

    List<IOrderType> OrderTypes { get; }
}