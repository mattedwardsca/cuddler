using System.ComponentModel.DataAnnotations;

namespace Cuddler.Web.Settings;

public class DropoffDeliverySettings
{
    [Required]
    public string DropoffDeliveryCalculator { get; set; } = nameof(FreeShippingRule);

    public bool EnableDeliverFridays { get; set; }

    public bool EnableDeliverMondays { get; set; }

    public bool EnableDeliverSaturdays { get; set; }

    public bool EnableDeliverSundays { get; set; }

    public bool EnableDeliverThursdays { get; set; }

    public bool EnableDeliverTuesdays { get; set; }

    public bool EnableDeliverWednesdays { get; set; }

    public bool EnableDropoffDelivery { get; set; }

    public int FirstAvailableDeliveryDate { get; set; }

    public int LastAvailableDeliveryDate { get; set; }
}