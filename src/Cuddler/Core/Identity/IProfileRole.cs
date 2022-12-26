namespace Cuddler.Core.Identity;

public interface IProfileRole
{
    bool IsAdvisor { get; set; }

    bool IsAuditor { get; set; }

    bool IsCoordinator { get; set; }

    bool IsOrganizationAdmin { get; set; }
}