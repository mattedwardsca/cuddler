﻿namespace Cuddler.Core.Services.Settings;

public class CustomShippingSettings
{
    public string? CustomShippingCalculator { get; set; }

    public bool EnableCustomShipping { get; set; } = true;
}