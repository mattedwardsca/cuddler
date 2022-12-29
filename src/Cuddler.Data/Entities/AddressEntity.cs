using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Addresses")]
public class AddressEntity : BaseEntity, IAddress, IRequiresContext
{
    public string? AddressType { get; set; }

    public string? AttentionContact { get; set; }

    public string? AttentionPhone { get; set; }

    public DateTime? DateMaintenanceRequested { get; set; }

    /// <summary>
    ///     Set to true when this is the default shipping address
    /// </summary>
    public bool Default { get; set; }

    public string? Description { get; set; }

    public string? Directions { get; set; }

    public string? Instance { get; set; }

    public string? PrivateNotes { get; set; }

    public string? ReferenceId { get; set; }

    public string? Tags { get; set; }

    [DisplayName("City")]
    public string? City { get; set; }

    public double DistanceFromStore { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    [DisplayName("Postal Code")]
    public string? PostalCode { get; set; }

    [DisplayName("Province")]
    public string? ProvinceCode { get; set; }

    public string? RegionId { get; set; }

    [DisplayName("Street / Blue Sign Number")]
    public string? Street1 { get; set; }

    public string? Street2 { get; set; }

    public string FormatAddress()
    {
        var firstItem = true;
        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(Street1))
        {
            sb.Append(Street1);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(Street2))
        {
            if (!string.IsNullOrEmpty(Street1))
            {
                sb.Append(", ");
            }

            sb.Append(Street2);
            firstItem = false;
        }

        if (!string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(ProvinceCode))
        {
            if (!firstItem)
            {
                sb.Append("<br/>");
            }

            sb.Append(City);
            if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(ProvinceCode))
            {
                sb.Append(", ");
            }

            sb.Append(ProvinceCode);

            firstItem = false;
        }

        if (!string.IsNullOrEmpty(PostalCode))
        {
            if (!firstItem)
            {
                sb.Append(", ");
            }

            sb.Append(PostalCode);
        }

        return sb.ToString();
    }

    [Required]
    [ValidateNever]
    public string ContextId { get; set; } = null!;

    [Required]
    [ValidateNever]
    public string ContextType { get; set; } = null!;
}