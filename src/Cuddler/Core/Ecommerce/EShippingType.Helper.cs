namespace Cuddler.Core.Ecommerce;

public static class EShippingTypeHelper
{
    public static string[] List()
    {
        return Enum.GetNames(typeof(EShippingType));
    }

    public static EShippingType Parse(string? parameter)
    {
        if (string.IsNullOrEmpty(parameter))
        {
            return EShippingType.None;
        }

        switch (parameter.ToLower())
        {
            case "canadapost":
            case "shipping":
                return EShippingType.CanadaPost;

            case "pickup":
                return EShippingType.Pickup;

            case "dropoff":
                return EShippingType.Dropoff;

            case "customshipping":
                return EShippingType.CustomShipping;

            default:
                return EShippingType.None;
        }
    }
}