using Cuddler.Data.Entities;

namespace Cuddler.Core.Services.Projects;

public interface IProjectsService
{
    List<OrderEntity> ListProjectOrders(ProjectEntity project);
}