using System.ComponentModel.DataAnnotations;

namespace CuddlerDev.Web.Settings;

public class CustomShippingSettings
{
    [Required]
    public string CustomShippingCalculator { get; set; } = nameof(FreeShippingRule);

    public bool EnableCustomShipping { get; set; } = true;
}