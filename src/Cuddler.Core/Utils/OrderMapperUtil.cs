using Cuddler.Core.Services.Settings;
using Cuddler.Data.Entities;

namespace Cuddler.Core.Utils;

public static class OrderMapperUtil
{
    public static void MapAccountInfo(OrderEntity order, AccountEntity? account)
    {
        order.OwnerId = account?.Id;
        order.ShippingContactName = account?.Profile.Name;
        order.ShippingContactEmail = account?.Email;
        order.ShippingContactPhone = account?.PhoneNumber;
    }

    public static void MapAccountInfo(OrderEntity order, OrganizationEntity organization)
    {
        // order.OrganizationId = organization.Id;
        // order.AccountProfileName = organization.OrganizationName;
        // order.AccountEmail = organization.BillingEmail;
        // order.AccountOrganizationName = organization.OrganizationName;
        // order.AccountPhoneNumber = organization.BillingPhone;
        throw new NotImplementedException("9b90e6b8-3c7f-4ee7-a4a3-b9de646c8b5e");
    }

    public static void MapWebsiteInfo(OrderEntity order, GlobalSettings invoiceSettings)
    {
        order.WebsiteName = invoiceSettings.WebsiteSettings.WebsiteName;
        order.WebsiteShippingStreet1 = invoiceSettings.WebsiteSettings.WebsiteShippingStreet1;
        order.WebsiteShippingStreet2 = invoiceSettings.WebsiteSettings.WebsiteShippingStreet2;
        order.WebsiteShippingCity = invoiceSettings.WebsiteSettings.WebsiteShippingCity;
        order.WebsiteShippingProvince = invoiceSettings.WebsiteSettings.WebsiteShippingProvince;
        order.WebsiteShippingLatitude = invoiceSettings.WebsiteSettings.WebsiteShippingLatitude;
        order.WebsiteShippingLongitude = invoiceSettings.WebsiteSettings.WebsiteShippingLongitude;
        order.WebsiteShippingPostalCode = invoiceSettings.WebsiteSettings.WebsiteShippingPostalCode;
        //billing
        order.WebsiteBillingStreet1 = invoiceSettings.WebsiteSettings.WebsiteBillingStreet1;
        order.WebsiteBillingStreet2 = invoiceSettings.WebsiteSettings.WebsiteBillingStreet2;
        order.WebsiteBillingCity = invoiceSettings.WebsiteSettings.WebsiteBillingCity;
        order.WebsiteBillingProvince = invoiceSettings.WebsiteSettings.WebsiteBillingProvince;
        order.WebsiteBillingPostalCode = invoiceSettings.WebsiteSettings.WebsiteBillingPostalCode;
        order.WebsiteBillingEmail = invoiceSettings.WebsiteSettings.WebsiteBillingEmail;
        order.WebsiteBillingPhone = invoiceSettings.WebsiteSettings.WebsiteBillingPhone;
    }
}