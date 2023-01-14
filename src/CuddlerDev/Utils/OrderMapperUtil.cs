using CuddlerDev.Data.Entities;
using CuddlerDev.Web.Settings;

namespace CuddlerDev.Utils;

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

    public static void MapWebsiteInfo(OrderEntity order, WebsiteSettings invoiceSettings)
    {
        order.WebsiteName = invoiceSettings.GeneralSettings.WebsiteName;
        order.WebsiteShippingStreet1 = invoiceSettings.GeneralSettings.WebsiteShippingStreet1;
        order.WebsiteShippingStreet2 = invoiceSettings.GeneralSettings.WebsiteShippingStreet2;
        order.WebsiteShippingCity = invoiceSettings.GeneralSettings.WebsiteShippingCity;
        order.WebsiteShippingProvince = invoiceSettings.GeneralSettings.WebsiteShippingProvince;
        order.WebsiteShippingLatitude = invoiceSettings.GeneralSettings.WebsiteShippingLatitude;
        order.WebsiteShippingLongitude = invoiceSettings.GeneralSettings.WebsiteShippingLongitude;
        order.WebsiteShippingPostalCode = invoiceSettings.GeneralSettings.WebsiteShippingPostalCode;
        //billing
        order.WebsiteBillingStreet1 = invoiceSettings.GeneralSettings.WebsiteBillingStreet1;
        order.WebsiteBillingStreet2 = invoiceSettings.GeneralSettings.WebsiteBillingStreet2;
        order.WebsiteBillingCity = invoiceSettings.GeneralSettings.WebsiteBillingCity;
        order.WebsiteBillingProvince = invoiceSettings.GeneralSettings.WebsiteBillingProvince;
        order.WebsiteBillingPostalCode = invoiceSettings.GeneralSettings.WebsiteBillingPostalCode;
        order.WebsiteBillingEmail = invoiceSettings.GeneralSettings.WebsiteBillingEmail;
        order.WebsiteBillingPhone = invoiceSettings.GeneralSettings.WebsiteBillingPhone;
    }
}