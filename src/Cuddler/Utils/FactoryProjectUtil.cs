using Cuddler.Core.Ecommerce;
using Cuddler.Core.Projects;

namespace Cuddler.Utils;

public static class FactoryProjectUtil
{
    public static IOrderType GetOrderType(IProjectType projectType, string orderTypeId)
    {
        return projectType.OrderTypes.Single(w => w.Id == orderTypeId);
    }
}