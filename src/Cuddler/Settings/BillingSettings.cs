namespace Cuddler.Settings;

public class BillingSettings
{
    public CustomShippingSettings CustomShippingSettings { get; set; } = new();

    public DropoffDeliverySettings DropoffDeliverySettings { get; set; } = new();

    public OrdersSettings OrdersSettings { get; set; } = new();

    public PaymentBankAccountSettings PaymentBankAccountSettings { get; set; } = new();

    public PickupSettings PickupSettings { get; set; } = new();

    public StatementSettings StatementSettings { get; set; } = new();

    public TaxFeesSettings TaxFeesSettings { get; set; } = new();
}