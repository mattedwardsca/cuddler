using Cuddler.Data.Entities;

namespace Cuddler.Core.Services.Docs;

public interface IDocsService
{
    void CreateAndSaveJsonFile(ProjectEntity project, string directory);

    ProjectEntity GetProjectFromJson(string projectId, string directory);

    string GetSerializedProject(ProjectEntity project);

    List<OrderEntity> ListFactoryModuleByProject(ProjectEntity entity);
}