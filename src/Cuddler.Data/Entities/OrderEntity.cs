using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Core_Orders")]
public class OrderEntity : BaseEntity, IHasName, IWebsiteBillingAddress, IHasDescription, ILockable, IHasWebsiteAddress, IHasShippingInfo, IHasToken, ISortable, IHasDateEmailed, IHasBillingAddress, IHasOrganization
{
    public virtual ProjectEntity? Project { get; set; }

    public bool AllowGuests { get; set; }

    public string? Category { get; set; }

    [ForeignKey(nameof(Project))]
    public string? ProjectId { get; set; }

    public bool Hidden { get; set; }

    public string? Icon { get; set; }

    public bool IsForOrganizations { get; set; }

    public bool IsForAdmins { get; set; }

    public bool IsForClients { get; set; }

    public bool IsPinnable { get; set; }

    public bool IsRequired { get; set; }

    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
    public virtual string Segment => Name == null
        ? string.Empty
        : Name.Replace(" ", string.Empty);

    public bool ShowInApps { get; set; }

    public bool IsAlwaysPinned { get; set; }

    public string? FactoryBoardId { get; set; }

    public string? BillingCompanyName { get; set; }

    public string? ClientsOrderName { get; set; }

    // ReSharper disable once Mvc.TemplateNotResolved
    [UIHint("ClientsStatus")]
    public string? ClientsStatus { get; set; }

    public string? Comments { get; set; }

    public string? Currency { get; set; }

    public DateTime? DateApproved { get; set; }

    public DateTime? DateCantCancel { get; set; }

    public DateTime? DateFulfilled { get; set; }

    public DateTime? DatePaid { get; set; }

    public DateTime? DateRejected { get; set; }

    public DateTime? DateRequestInfo { get; set; }

    public DateTime? DateSubmitted { get; set; }

    public DateTime? DateSubmittedToBilling { get; set; }

    public DateTime? DateSubmittedToCoordinator { get; set; }

    public string? GuestId { get; set; }

    public string? InvoiceId { get; set; }

    public bool IsDraft { get; set; }

    public DateTime? PlanningToBuyDate { get; set; }


    public string? ShippingAddressId { get; set; }

    public string? Status { get; set; }

    public bool WasClientInitiated { get; set; }

    [ValidateNever]
    [Required]
    public string OrderTypeId { get; set; } = null!;

    public DateTime? PaymentDueDate { get; set; }

    public bool ShippingOverridden { get; set; }

    public string? StatusInfo { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal TotalDiscountAsAmount { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal TotalDiscountAsPercent { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal TotalTaxExempt { get; set; }

    public virtual decimal TotalTaxableAmount => TotalChargesBeforeTax - TotalTaxExempt;

    [Column(TypeName = "decimal(18,3)")]
    public decimal TotalShipping { get; set; }

    public string? WebsiteShippingCity { get; set; }

    public string? WebsiteShippingProvince { get; set; }

    public DateTime? DisplayDate { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal TotalChargesBeforeTax { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal TotalIncludingTax { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal TotalTaxAmount { get; set; }

    public virtual OrganizationEntity? Organization { get; set; }

    public virtual AccountEntity? Owner { get; set; }

    public virtual ScheduleEntity? Schedule { get; set; }

    [ForeignKey(nameof(Schedule))]
    public string? ScheduleId { get; set; }

    public string? BillingCity { get; set; }

    public string? BillingContactName { get; set; }

    public string? BillingPostalCode { get; set; }

    public string? BillingProvinceCode { get; set; }

    public string? BillingStreet1 { get; set; }

    public string? BillingStreet2 { get; set; }

    public string? BillingType { get; set; }

    public DateTime? DateEmailed { get; set; }

    public string? Description { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? OrganizationId { get; set; }

    public string? OwnerId { get; set; } 

    public DateTime? DateShipped { get; set; }

    public string? ShippingCity { get; set; }

    public string? ShippingContactCompanyName { get; set; }

    public string? ShippingContactEmail { get; set; }

    public string? ShippingContactName { get; set; }

    public string? ShippingContactPhone { get; set; }

    public double ShippingDistanceFromStore { get; set; }

    public DateTime? ShippingEndTime { get; set; }

    public DateTime? ShippingEstimatedDeliveryDate { get; set; }

    public double ShippingLatitude { get; set; }

    public double ShippingLongitude { get; set; }

    public DateTime? ShippingPickupDate { get; set; }

    public string? ShippingPostalCode { get; set; }

    public string? ShippingProvinceCode { get; set; }

    // public virtual ShippingRegionEntity? ShippingRegion { get; set; }

    // [ForeignKey(nameof(ShippingRegion))]
    public string? ShippingRegionId { get; set; }

    public string? ShippingRouteId { get; set; }

    public string? ShippingRule { get; set; }

    public string? ShippingServiceCode { get; set; }

    public string? ShippingServiceName { get; set; }

    public string? ShippingShiftId { get; set; }

    public DateTime? ShippingStartTime { get; set; }

    public string? ShippingStreet1 { get; set; }

    public string? ShippingStreet2 { get; set; }

    public string? ShippingType { get; set; }

    public string ToShippingAddress()
    {
        var firstItem = true;
        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(ShippingStreet1))
        {
            sb.Append(ShippingStreet1);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(ShippingStreet2))
        {
            if (!string.IsNullOrEmpty(ShippingStreet1))
            {
                sb.Append(", ");
            }

            sb.Append(ShippingStreet2);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(ShippingCity) || !string.IsNullOrEmpty(ShippingProvinceCode))
        {
            if (!firstItem)
            {
                sb.Append("<br/>");
            }

            sb.Append(ShippingCity);
            if (!string.IsNullOrEmpty(ShippingCity) && !string.IsNullOrEmpty(ShippingProvinceCode))
            {
                sb.Append(", ");
            }

            sb.Append(ShippingProvinceCode);

            firstItem = false;
        }

        if (!string.IsNullOrEmpty(ShippingPostalCode))
        {
            if (!firstItem)
            {
                sb.Append(", ");
            }

            sb.Append(ShippingPostalCode);
        }

        return sb.ToString();
    }

    [Required]
    [ValidateNever]
    public string Token { get; set; } = null!;

    public double WebsiteShippingLatitude { get; set; }

    public double WebsiteShippingLongitude { get; set; }

    public string? WebsiteShippingPostalCode { get; set; }

    public string? WebsiteShippingStreet1 { get; set; }

    public string? WebsiteShippingStreet2 { get; set; }

    public DateTime? DateLocked { get; set; }

    public bool IsLocked()
    {
        return DateLocked != null && DateTime.UtcNow.ToLocalTime() > DateLocked;
    }

    public int SortOrder { get; set; }

    public string? WebsiteBillingEmail { get; set; }

    public string? WebsiteBillingPhone { get; set; }

    public string? WebsiteBillingPostalCode { get; set; }

    public string? WebsiteBillingProvince { get; set; }

    public string? WebsiteBillingStreet1 { get; set; }

    public string? WebsiteBillingStreet2 { get; set; }

    public string? WebsiteName { get; set; }

    public string? WebsiteBillingCity { get; set; }

    public virtual bool CustomerCanCancel()
    {
        return DateArchived == null && (DateCantCancel == null || DateCantCancel > DateTime.UtcNow.ToLocalTime()) && !IsLocked();
    }

    public virtual bool CustomerCanReschedule()
    {
        return DateArchived == null && (DateLocked == null || DateLocked > DateTime.UtcNow.ToLocalTime());
    }

    public bool IsApproved()
    {
        return DateApproved != null && DateTime.UtcNow.ToLocalTime() > DateApproved;
    }

    public bool IsCanCancell()
    {
        return DateArchived == null && InvoiceId == null;
    }

    public bool IsCancelled()
    {
        return DateArchived != null;
    }

    public bool IsDelivered()
    {
        return DateShipped != null && DateTime.UtcNow.ToLocalTime() >= DateShipped;
    }

    public void SaveAsCompleted()
    {
        if (DateCantCancel == null || DateCantCancel > DateTime.UtcNow.ToLocalTime())
        {
            DateCantCancel = DateTime.UtcNow.ToLocalTime();
        }

        if (DateLocked == null || DateLocked > DateTime.UtcNow.ToLocalTime())
        {
            DateLocked = DateTime.UtcNow.ToLocalTime();
        }
    }

    public override string ToString()
    {
        return $"{Token}";
    }
}