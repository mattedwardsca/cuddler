namespace Cuddler.Web.Settings;

public interface IDeliveryRule
{
    decimal CalculateShipping(string deliveryType, decimal subtotal);
}