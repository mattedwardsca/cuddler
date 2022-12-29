using Cuddler.Forms.Ui;

namespace Cuddler.Pages.Shared.Cuddler.ActionMenu;

public static class ActionMenuItemsExtensions
{
    public static ActionMenuItems ActionMenu(this CuddlerUi cuddler)
    {
        return new ActionMenuItems(cuddler.HtmlHelper);
    }
}