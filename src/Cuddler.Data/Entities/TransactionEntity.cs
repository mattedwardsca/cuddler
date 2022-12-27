using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cuddler.Data.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Core_Transactions")]
public class TransactionEntity : BaseEntity, ISortable, IHasOwner, IHasFormModel, IHasToken
{
    public string? BillId { get; set; }

    public string? CategoryId { get; set; }

    public string? ClientsTitle { get; set; }

    public string? Currency { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public string? DocumentId { get; set; }

    public DateTime? EndDate { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Gst { get; set; }

    public bool HasSalesTax { get; set; }

    public string? InventoryId { get; set; }

    public string? Name { get; set; }

    public virtual OrderEntity? Order { get; set; }

    [ForeignKey(nameof(Order))]
    public string? OrderId { get; set; }

    public virtual AccountEntity? Owner { get; set; }

    public ProductEntity? Product { get; set; }

    [ForeignKey(nameof(Product))]
    public string? ProductId { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Pst { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Quantity { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal? RecyclingFee { get; set; }

    public string? Sku { get; set; }

    public DateTime? StartDate { get; set; }

    public string? Status { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Subtotal { get; set; }

    public string? SupplierId { get; set; }

    public string? ThumbnailId { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Total { get; set; }

    [Required]
    [ValidateNever]
    public string UnitOfMeasure { get; set; } = null!;

    [Column(TypeName = "decimal(18,3)")]
    public decimal UnitPrice { get; set; }

    public string? ModelDetails { get; set; }

    public string? SerializedModel { get; set; }

    public string? SerializedType { get; set; }

    public IFormModel? DeserializeModel()
    {
        if (string.IsNullOrEmpty(SerializedType) || string.IsNullOrEmpty(SerializedModel))
        {
            return default;
        }

        var formModelType = Type.GetType(SerializedType);
        if (formModelType == null)
        {
            return default;
        }

        return (IFormModel)SerializationUtil.JsonDeserializeObject(formModelType, SerializedModel);
    }

    public void SerializeModel(object obj)
    {
        if (string.IsNullOrEmpty(SerializedType))
        {
            throw new InvalidOperationException("Cannot serialize an unknown type. Error: d10327c1-37ee-4f3b-b31b-02a7601f1d4e");
        }

        SerializedModel = SerializationUtil.JsonSerializeObject(obj);
    }

    public string? OwnerId { get; set; }

    [Required]
    [ValidateNever]
    public string Token { get; set; } = null!;

    public int SortOrder { get; set; }

    public decimal GetItemPrice()
    {
        var result = UnitPrice * Quantity;

        return Math.Round(result, 2, MidpointRounding.ToEven);
    }

    // public string Gst { get; set; }
    public string GetPeriod()
    {
        if (StartDate.HasValue && EndDate.HasValue)
        {
            return $"{FormatDateUtil.FormatJsonDate(StartDate.Value)} to {FormatDateUtil.FormatJsonDate(EndDate.Value)}";
        }

        if (StartDate.HasValue && !EndDate.HasValue)
        {
            return $"{FormatDateUtil.FormatJsonDate(StartDate.Value)} to {FormatDateUtil.FormatJsonDate(StartDate.Value)}";
        }

        return $"{FormatDateUtil.FormatJsonDate(Date)}";
    }
}