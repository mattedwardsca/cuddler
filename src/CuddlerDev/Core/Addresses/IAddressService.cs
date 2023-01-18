using CuddlerDev.Data.Entities;

namespace CuddlerDev.Core.Addresses;

public interface IAddressService
{
    AddressEntity? GetBillingAddress(OrganizationEntity organization);

    Task<AddressEntity> GetRequiredBillingAddress(AccountEntity account);

    Task<AddressEntity> GetRequiredDefaultDeliveryAddress(OrganizationEntity organization);

    Task<AddressEntity> GetRequiredDefaultDeliveryAddress(AccountEntity account);

    IQueryable<AddressEntity> ListShippingAddresses(string contextId);

    AddressEntity SaveAddress(IAddress model, string contextId, string contextType, EAddressType addressType);
    
    AddressEntity GetAddress(string addressId);
}