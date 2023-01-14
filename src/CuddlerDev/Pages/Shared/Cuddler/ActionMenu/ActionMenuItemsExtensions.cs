namespace CuddlerDev.Pages.Shared.Cuddler.ActionMenu;

public static class ActionMenuItemsExtensions
{
    public static ActionMenuItems ActionMenu(this Ui.CuddlerUi cuddler)
    {
        return new ActionMenuItems(cuddler.HtmlHelper);
    }
}