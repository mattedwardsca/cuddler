namespace CuddlerDev.Web.Helpers;

public static class EBadgeTypeHelper
{
    public static string ToCss(EBadgeType badgeType)
    {
        return badgeType switch
        {
            EBadgeType.Primary => "badge badge-pill bg-primary",
            EBadgeType.Success => "badge badge-pill bg-success",
            EBadgeType.Warning => "badge badge-pill bg-warning",
            EBadgeType.Danger => "badge badge-pill bg-danger",
            EBadgeType.Secondary => "badge badge-pill bg-secondary",
            EBadgeType.Info => "badge badge-pill bg-info",
            _ => Enum.GetName(typeof(EBadgeType), badgeType) ?? "badge badge-pill bg-info"
        };
    }
}