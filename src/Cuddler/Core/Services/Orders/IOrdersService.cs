using Cuddler.Core.Helpers;
using Cuddler.Data.Entities;

namespace Cuddler.Core.Services.Orders;

public interface IOrdersService
{
    TransactionEntity AddCustomItem(OrderEntity order, TransactionEntity orderItem);

    TransactionEntity AddOrderProduct(OrderEntity order, ProductEntity product, decimal quantity);

    TransactionEntity CreateDefaultOrderProduct(OrderEntity orderEntity);

    Task<OrderEntity> CreateOrder(AccountEntity account, EShippingType deliveryType, EBillingType billingType);

    Task<bool> IsAllowOrderChanges(OrderEntity order, AccountEntity account);

    IQueryable<OrderEntity> ListActiveOrders(DateTime? start, DateTime? end);

    IQueryable<OrderEntity> ListArchivedOrders(DateTime? start, DateTime? end);

    Task<IQueryable<OrderEntity>> ListCancelledCarts(DateTime? startdate, DateTime? enddate);

    IQueryable<OrderEntity> ListCartEstimates(string?[] ids);

    IQueryable<OrderEntity> ListCartsNotCheckedOut(DateTime start, DateTime end);

    IQueryable<OrderEntity> ListCompletedOrders(DateTime? start, DateTime? end);

    void MapAccountInfo(OrderEntity order, AccountEntity account);

    void MapBillingInfo(OrderEntity invoice, IAddress? address, EBillingType billingType);

    void MapDeliveryInfo(OrderEntity cart, IAddress deliveryAddress, EShippingType deliveryType, string? deliveryContactEmail, string? deliveryContactName);

    Task<TransactionEntity> UpdateOrder(TransactionEntity order, IDictionary<string, object?> dictionary, AccountEntity account);

    void UpdateOrderProductMeta(OrderEntity estimate);
}