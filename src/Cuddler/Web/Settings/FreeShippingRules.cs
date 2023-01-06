namespace Cuddler.Web.Settings;

public class FreeShippingRule : IDeliveryRule
{
    public string Instructions => "Free shipping on all orders";

    public string Name => "Free Shipping";

    public string Type => GetType()
        .Name;

    public decimal CalculateShipping(string deliveryType, decimal subtotal)
    {
        return subtotal * 0;
    }

    public override string ToString()
    {
        return Type;
    }
}