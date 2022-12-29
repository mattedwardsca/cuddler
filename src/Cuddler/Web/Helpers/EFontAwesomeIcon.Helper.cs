using Cuddler.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuddler.Helpers;

public static class EFontAwesomeIconHelper
{
    public static string[] List()
    {
        return Enum.GetNames(typeof(EFontAwesomeIcon));
    }

    public static EFontAwesomeIcon Parse(string sEnum)
    {
        return (EFontAwesomeIcon)Enum.Parse(typeof(EFontAwesomeIcon), sEnum, true);
    }

    public static List<SelectListItem> SelectList()
    {
        var strings = List();

        return strings.Select(s => new SelectListItem(StringUtil.SplitCamelCase(s), ToString(Parse(s))))
                      .ToList();
    }

    public static string ToCss(EFontAwesomeIcon fontAwesomeIcon)
    {
        return ToString(fontAwesomeIcon);
    }

    public static string ToString(EFontAwesomeIcon? icon)
    {
        if (icon == null)
        {
            return string.Empty;
        }

        return icon switch
        {
            EFontAwesomeIcon.AddressBook => "fas fa-address-book",
            EFontAwesomeIcon.Apps => "fad fa-th",
            EFontAwesomeIcon.Archive => "fas fa-archive",
            EFontAwesomeIcon.ArchiveRestore => "fas fa-trash-restore-alt",
            EFontAwesomeIcon.Ban => "fas fa-ban",
            EFontAwesomeIcon.Blocks => "fas fa-th",
            EFontAwesomeIcon.Boxes => "fas fa-boxes",
            EFontAwesomeIcon.Business => "fas fa-building",
            EFontAwesomeIcon.Calendar => "fas fa-calendar",
            EFontAwesomeIcon.Cancel => "fas fa-times",
            EFontAwesomeIcon.CartPlus => "fas fa-cart-plus",
            EFontAwesomeIcon.Catalogue => "fad fa-boxes",
            EFontAwesomeIcon.ChartPie => "fas fa-chart-pie",
            EFontAwesomeIcon.Checkmark => "fas fa-check",
            EFontAwesomeIcon.Columns => "fas fa-columns",
            EFontAwesomeIcon.Customers => "fad fa-address-book",
            EFontAwesomeIcon.Danger => "fas fa-exclamation-circle",
            EFontAwesomeIcon.Dashboards => "fad fa-tachometer-alt-fast",
            EFontAwesomeIcon.Deliveries => "fad fa-truck",
            EFontAwesomeIcon.Disabled => "fas fa-ban",
            EFontAwesomeIcon.Download => "fas fa-file-download",
            EFontAwesomeIcon.Edit => "fas fa-pencil",
            EFontAwesomeIcon.EditRow => "fas fa-edit",
            EFontAwesomeIcon.Ellipsis => "fas fa-ellipsis-v-alt",
            EFontAwesomeIcon.Email => "fas fa-envelope",
            EFontAwesomeIcon.Emails => "fad fa-paper-plane",
            EFontAwesomeIcon.Employees => "fad fa-users",
            EFontAwesomeIcon.Envelope => "fas fa-envelope",
            EFontAwesomeIcon.Expenses => "fad fa-file-contract",
            EFontAwesomeIcon.GripVertical => "fas fa-grip-vertical",
            EFontAwesomeIcon.Help => "fas fa-question-circle",
            EFontAwesomeIcon.History => "fas fa-clock-three",
            EFontAwesomeIcon.Info => "fas fa-info-circle",
            EFontAwesomeIcon.Invoice => "fas fa-file-invoice",
            EFontAwesomeIcon.Key => "fas fa-key",
            EFontAwesomeIcon.Link => "fas fa-link",
            EFontAwesomeIcon.List => "fas fa-th-list",
            EFontAwesomeIcon.Locked => "fas fa-lock-alt",
            EFontAwesomeIcon.Messaging => "fad fa-envelope",
            EFontAwesomeIcon.MyAccount => "fad fa-user-circle",
            EFontAwesomeIcon.None => string.Empty,
            EFontAwesomeIcon.OrderHistory => "fad fa-search-dollar",
            EFontAwesomeIcon.Ordering => "fad fa-file-invoice",
            EFontAwesomeIcon.Phone => "fas fa-phone",
            EFontAwesomeIcon.Plus => "fas fa-plus-circle",
            EFontAwesomeIcon.Pointer => "fas fa-hand-pointer",
            EFontAwesomeIcon.Print => "fas fa-print",
            EFontAwesomeIcon.Project => "fas fa-project-diagram",
            EFontAwesomeIcon.Projects => "fad fa-project-diagram",
            EFontAwesomeIcon.Purchasing => "fad fa-money-check-alt",
            EFontAwesomeIcon.Receipt => "fas fa-receipt",
            EFontAwesomeIcon.Redo => "fas fa-redo-alt",
            EFontAwesomeIcon.RelocationApp => "fas fa-chart-network",
            EFontAwesomeIcon.Report => "fas fa-file-chart-pie",
            EFontAwesomeIcon.Reporting => "fad fa-file-chart-pie",
            EFontAwesomeIcon.Reset => "fas fa-broom",
            EFontAwesomeIcon.RightArrow => "fas fa-arrow-right",
            EFontAwesomeIcon.LeftArrow => "fas fa-arrow-left",
            EFontAwesomeIcon.Robot => "fas fa-user-robot",
            EFontAwesomeIcon.Save => "fas fa-save",
            EFontAwesomeIcon.Setting => "fas fa-cog",
            EFontAwesomeIcon.Settings => "fad fa-cogs",
            EFontAwesomeIcon.ShopCart => "fas fa-shopping-cart",
            EFontAwesomeIcon.ShopCategories => "fas fa-bags-shopping",
            EFontAwesomeIcon.ShopPackages => "fas fa-bags-shopping",
            EFontAwesomeIcon.ShopTags => "fas fa-bags-shopping",
            EFontAwesomeIcon.Tag => "fas fa-tag",
            EFontAwesomeIcon.Spy => "fas fa-user-secret",
            EFontAwesomeIcon.Star => "fas fa-star",
            EFontAwesomeIcon.Stations => "fad fa-space-station-moon-alt",
            EFontAwesomeIcon.Sync => "fad fa-sync",
            EFontAwesomeIcon.Tasks => "fad fa-tasks",
            EFontAwesomeIcon.Trash => "fas fa-trash",
            EFontAwesomeIcon.TrashRestore => "fas fa-trash-restore-alt",
            EFontAwesomeIcon.Undo => "fas fa-undo-alt",
            EFontAwesomeIcon.Unlocked => "fas fa-lock-open-alt",
            EFontAwesomeIcon.Upload => "fas fa-upload",
            EFontAwesomeIcon.User => "fas fa-user",
            EFontAwesomeIcon.Users => "fas fa-users",
            EFontAwesomeIcon.UserShield => "fas fa-user-shield",
            EFontAwesomeIcon.Warning => "fas fa-exclamation-triangle",
            EFontAwesomeIcon.Website => "fad fa-browser",
            EFontAwesomeIcon.Webstore => "fas fa-bags-shopping",
            EFontAwesomeIcon.Window => "fas fa-window",
            EFontAwesomeIcon.Wizard => "fas fa-hat-wizard",
            EFontAwesomeIcon.CheckIn => "fas fa-arrow-right-to-arc",
            EFontAwesomeIcon.CheckOut => "fas fa-arrow-right-from-arc",
            EFontAwesomeIcon.Visitor => "fas fa-person-circle-question",
            EFontAwesomeIcon.Incident => "fas fa-triangle-exclamation",
            EFontAwesomeIcon.Emergency => "fas fa-house-fire",
            EFontAwesomeIcon.Tools => "fas fa-screwdriver-wrench",
            EFontAwesomeIcon.Housekeeping => "fas fa-vacuum",
            EFontAwesomeIcon.Laundry => "fas fa-dryer",
            EFontAwesomeIcon.Feedback => "fas fa-comment-smile",
            EFontAwesomeIcon.Luggage => "fas fa-suitcase-rolling",
            EFontAwesomeIcon.LinkNewTab => "fas fa-up-right-from-square",
            EFontAwesomeIcon.Pdf => "fas fa-file-pdf",
            EFontAwesomeIcon.Box => "fas fa-box",
            EFontAwesomeIcon.Wallet => "fas fa-wallet",
            EFontAwesomeIcon.ArrowsToEye => "fas fa-arrows-to-eye",
            EFontAwesomeIcon.Graduate => "fas fa-user-graduate",
            EFontAwesomeIcon.MilitaryUser => "fas fa-person-military-pointing",
            EFontAwesomeIcon.UserCrown => "fas fa-user-crown",
            EFontAwesomeIcon.Flag => "fas fa-flag-pennant",
            EFontAwesomeIcon.Square => "fa-regular fa-square",

            var _ => throw new ArgumentOutOfRangeException(nameof(icon), icon, $"Icon '{icon}' is not supported. (Error: fa4daeaf-8438-40f0-a522-befbac78902c)")
        };
    }
}