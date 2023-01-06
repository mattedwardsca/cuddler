using System.ComponentModel.DataAnnotations;

namespace Cuddler.Web.Settings;

public class CustomShippingSettings
{
    [Required]
    public string CustomShippingCalculator { get; set; } = nameof(FreeShippingRule);

    public bool EnableCustomShipping { get; set; } = true;
}