using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cuddler.Web.Settings;

public class OrdersSettings
{
    [Required]
    public string OrderPrefix { get; set; } = "ORD";

    [DisplayName("Show Payment Information On Quotes")]
    public bool ShowPaymentInformationOnQuotes { get; set; }
}