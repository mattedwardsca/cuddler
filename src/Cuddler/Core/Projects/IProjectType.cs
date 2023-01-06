using Cuddler.Core.Ecommerce;

namespace Cuddler.Core.Projects;

public interface IProjectType
{
    string Id { get; }

    string Name { get; }

    string OrderName { get; }

    List<IOrderType> OrderTypes { get; }
}