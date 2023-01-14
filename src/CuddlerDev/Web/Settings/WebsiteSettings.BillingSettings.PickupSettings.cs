using System.ComponentModel.DataAnnotations;
using CuddlerDev.Forms.Attributes;

namespace CuddlerDev.Web.Settings;

public class PickupSettings
{
    [Row(2, 0)]
    [Required]
    public string DeliveryCalculator { get; set; } = nameof(FreeShippingRule);

    [Row(1, 0)]
    public bool EnablePickup { get; set; }

    [Row(8, 0)]
    public bool PickupFridays { get; set; }

    [Row(4, 0)]
    public bool PickupMondays { get; set; }

    [Row(9, 0)]
    public bool PickupSaturdays { get; set; }

    [Row(3, 0)]
    public bool PickupSundays { get; set; }

    [Row(7, 0)]
    public bool PickupThursdays { get; set; }

    [Row(5, 0)]
    public bool PickupTuesdays { get; set; }

    [Row(6, 0)]
    public bool PickupWednesdays { get; set; }
}