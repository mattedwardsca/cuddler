using CuddlerDev.Core.Ecommerce;
using CuddlerDev.Core.Projects;

namespace CuddlerDev.Utils;

public static class FactoryProjectUtil
{
    public static IOrderType GetOrderType(IProjectType projectType, string orderTypeId)
    {
        return projectType.OrderTypes.Single(w => w.Id == orderTypeId);
    }
}