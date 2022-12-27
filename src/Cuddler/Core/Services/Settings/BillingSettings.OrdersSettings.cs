using System.ComponentModel;

namespace Cuddler.Core.Services.Settings;

public class OrdersSettings
{
    public string? OrderPrefix { get; set; } = "ORD";

    [DisplayName("Show Payment Information On Quotes")]
    public bool ShowPaymentInformationOnQuotes { get; set; }
}