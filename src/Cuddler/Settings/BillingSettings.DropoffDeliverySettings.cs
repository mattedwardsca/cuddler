namespace Cuddler.Settings;

public class DropoffDeliverySettings
{
    public string? DropoffDeliveryCalculator { get; set; }

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