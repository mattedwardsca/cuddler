namespace Cuddler.Data.Entities;

public interface IHasShippingInfo
{
    public DateTime? DateShipped { get; set; }

    // ScheduleEntity? Schedule { get; set; }

    string? ShippingCity { get; set; }

    string? ShippingContactCompanyName { get; set; }

    string? ShippingContactEmail { get; set; }

    string? ShippingContactName { get; set; }

    string? ShippingContactPhone { get; set; }

    double ShippingDistanceFromStore { get; set; }

    DateTime? ShippingEndTime { get; set; }

    DateTime? ShippingEstimatedDeliveryDate { get; set; }

    double ShippingLatitude { get; set; }

    double ShippingLongitude { get; set; }

    DateTime? ShippingPickupDate { get; set; }

    string? ShippingPostalCode { get; set; }

    string? ShippingProvinceCode { get; set; }

    // ShippingRegionEntity? ShippingRegion { get; set; }

    string? ShippingRegionId { get; set; }

    string? ShippingRouteId { get; set; }

    string? ShippingRule { get; set; }

    string? ShippingServiceCode { get; set; }

    string? ShippingServiceName { get; set; }

    string? ShippingShiftId { get; set; }

    DateTime? ShippingStartTime { get; set; }

    string? ShippingStreet1 { get; set; }

    string? ShippingStreet2 { get; set; }

    string? ShippingType { get; set; }

    string ToShippingAddress();
}