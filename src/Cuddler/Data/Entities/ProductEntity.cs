using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Products")]
public class ProductEntity : BaseEntity, IHasThumbnailId, IHasName
{
    public string? Description { get; set; }

    [DisplayName("About")]
    public string? Details { get; set; }

    public string? DistributorLink { get; set; }

    public string? DistributorName { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal EstimatedUnits { get; set; } = 1;

    public bool Featured { get; set; }

    public bool HasSalesTax { get; set; }

    public bool IsAdminFavourite { get; set; }

    public int? MaxOrder { get; set; }

    public int MinOrder { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? PriceGroupId { get; set; }

    [DisplayName("Categories")]
    public string? ProductCategoryId { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal RecyclingFee { get; set; }

    public bool ShowInStore { get; set; }

    public string? Sku { get; set; }

    public string[]? Specs { get; set; }

    public string? Tags { get; set; }

    public virtual string TaxesFees
    {
        get
        {
            var taxes = new List<string>();
            if (HasSalesTax)
            {
                taxes.Add("Sales Tax</br>");
            }

            return string.Join(", ", taxes);
        }
    }

    public string? TrackingCode { get; set; }

    // ie. Number of Litres
    [Column(TypeName = "decimal(18,3)")]
    public decimal UnitCost { get; set; }

    [Required]
    public string UnitOfMeasure { get; set; } = null!;

    [Column(TypeName = "decimal(18,3)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal? UnitPriceAdditional { get; set; }

    public string[]? RequiredAddons { get; set; }

    public string[]? OptionalAddons { get; set; }

    [DisplayName("Image")]
    public string? ThumbnailId { get; set; }

    public static decimal GetPrice(ProductEntity product, int numberItems)
    {
        var singlePrice = product.UnitPrice;
        var additionalPrice = product.UnitPriceAdditional ?? product.UnitPrice;

        return singlePrice + additionalPrice * (numberItems - 1);
    }

    public decimal GetItemPrice()
    {
        return UnitPrice * EstimatedUnits;
    }

    public string GetSlug()
    {
        return Name.Replace(" ", string.Empty)
                   .Replace("/", "");
    }

    public decimal GetTotalProfit()
    {
        return (UnitPrice - UnitCost) * EstimatedUnits;
    }

    public decimal GetUnitProfit()
    {
        return UnitPrice - UnitCost;
    }
}