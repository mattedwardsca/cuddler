namespace CuddlerDev.Core.Ecommerce;

public static class EBillingTypeHelper
{
    public static string[] List()
    {
        return Enum.GetNames(typeof(EBillingType));
    }

    public static EBillingType Parse(string? sEnum)
    {
        if (string.IsNullOrEmpty(sEnum))
        {
            return EBillingType.None;
        }

        return (EBillingType)Enum.Parse(typeof(EBillingType), sEnum, true);
    }

    public static string ToString(EBillingType eEnum)
    {
        switch (eEnum)
        {
            case EBillingType.CreditCard:
                return "Credit Card";

            case EBillingType.ETransfer:
                return "E-transfer";

            default:
                throw new ArgumentOutOfRangeException(nameof(eEnum), eEnum, null);
        }
    }
}