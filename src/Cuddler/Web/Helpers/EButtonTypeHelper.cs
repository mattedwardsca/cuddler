namespace Cuddler.Web.Helpers;

public static class EButtonTypeHelper
{
    public static string ToString(EButtonType buttonType)
    {
        return buttonType switch
        {
            EButtonType.Primary => "btn btn-primary",
            EButtonType.Secondary => "btn btn-secondary",
            EButtonType.Success => "btn btn-success",
            EButtonType.Warning => "btn btn-warning",
            EButtonType.ActionMenu => "eux-link",
            EButtonType.Link => "eux-link",
            EButtonType.Reminder => "eux-link",
            EButtonType.Danger => "btn btn-danger",
            EButtonType.Dark => "btn btn-dark",
            EButtonType.Light => "btn btn-light",
            EButtonType.Toolbar => "btn btn-toolbar",
            EButtonType.Icon => "btn btn-icon",
            EButtonType.IconWarning => "btn btn-icon btn-icon-warning",
            EButtonType.IconDanger => "btn btn-icon btn-icon-danger",
            EButtonType.IconJumbo => "btn btn-primary btn-jumbo",
            var _ => throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, "6f67bff8-688c-4ac0-8666-41f866d8ce73")
        };
    }
}