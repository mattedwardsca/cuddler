namespace CuddlerDev.Pages.Shared.Cuddler.IconPill;

public static class EIconPillTypeHelper
{
    public static string ToCss(EIconPillType iconPillType)
    {
        return iconPillType switch
        {
            EIconPillType.Primary => "badge badge-pill bg-primary",
            EIconPillType.Success => "badge badge-pill bg-success",
            EIconPillType.Warning => "badge badge-pill bg-warning",
            EIconPillType.Danger => "badge badge-pill bg-danger",
            EIconPillType.Secondary => "badge badge-pill bg-secondary",
            EIconPillType.Info => "badge badge-pill bg-info",
            _ => Enum.GetName(typeof(EIconPillType), iconPillType) ?? "badge badge-pill bg-info"
        };
    }
}