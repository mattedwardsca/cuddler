using CuddlerDev.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CuddlerDev.Core.Projects;

public interface IProjectTypesService
{
    List<IProjectType> ProjectTypes { get; set; }

    IProjectType GetProjectType(ProjectEntity project);

    List<SelectListItem> GetProjectTypeSelectList();
}