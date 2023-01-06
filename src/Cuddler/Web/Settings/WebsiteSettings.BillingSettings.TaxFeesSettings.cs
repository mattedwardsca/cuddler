using System.ComponentModel;

namespace Cuddler.Web.Settings;

public class TaxFeesSettings
{
    [DisplayName("Enable Deposit/Recycling Fees")]
    [Description("Do you charge deposit/recycling fees?")]
    public bool EnableRecyclingFees { get; set; }

    [Description("Some stores don't need to charge sales tax on all of their products and might not want it charged by default.")]
    [DisplayName("Charge Sales Tax by default")]
    public bool EnableSalesTax { get; set; }
}