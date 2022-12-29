using System.Text;
using Cuddler.Data.Entities;

namespace Cuddler.Web.Utils;

public static class AddressExtensions
{
    public static string ToBillingAddress(this IBillingAddress address)
    {
        return Address(false, address.BillingStreet1, address.BillingStreet2, address.BillingCity, address.BillingProvince, address.BillingPostalCode);
    }

    public static string ToBillingAddressHtml(this IBillingAddress address)
    {
        return Address(true, address.BillingStreet1, address.BillingStreet2, address.BillingCity, address.BillingProvince, address.BillingPostalCode);
    }

    public static string ToWebsiteBillingAddress(this IWebsiteBillingAddress address)
    {
        return Address(false, address.WebsiteBillingStreet1, address.WebsiteBillingStreet2, address.WebsiteBillingCity, address.WebsiteBillingProvince, address.WebsiteBillingPostalCode);
    }

    public static string ToWebsiteBillingAddressHtml(this IWebsiteBillingAddress address)
    {
        return Address(true, address.WebsiteBillingStreet1, address.WebsiteBillingStreet2, address.WebsiteBillingCity, address.WebsiteBillingProvince, address.WebsiteBillingPostalCode);
    }

    //public static string ToWebsiteShippingAddress(this IWebsiteShippingAddress address)
    //{
    //    return Address(false, address.WebsiteShippingStreet1, address.WebsiteShippingStreet2, address.WebsiteShippingCity, address.WebsiteShippingProvince, address.WebsiteShippingPostalCode);
    //}


    //public static string ToWebsiteShippingAddressHtml(this IWebsiteShippingAddress address)
    //{
    //    return Address(true, address.WebsiteShippingStreet1, address.WebsiteShippingStreet2, address.WebsiteShippingCity, address.WebsiteShippingProvince, address.WebsiteShippingPostalCode);
    //}

    public static string Address(bool asHtml, string? street1, string? street2, string? city, string? province, string? postalCode)
    {
        var firstItem = true;
        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(street1))
        {
            sb.Append(street1);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(street2))
        {
            if (!string.IsNullOrEmpty(street1))
            {
                sb.Append(", ");
            }

            sb.Append(street2);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(province))
        {
            if (!firstItem)
            {
                sb.Append(asHtml
                    ? "<br/>"
                    : "\n");
            }

            sb.Append(city);
            if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(province))
            {
                sb.Append(", ");
            }

            sb.Append(province);

            firstItem = false;
        }

        if (!string.IsNullOrEmpty(postalCode))
        {
            if (!firstItem)
            {
                sb.Append(", ");
            }

            sb.Append(postalCode);
        }

        return sb.ToString();
    }
}