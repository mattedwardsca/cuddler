using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuddlerDev.Data.Entities;

[Table("Cuddler_Suppliers")]
public class SupplierEntity : BaseEntity, IHasBalance, IHasDescription, IData
{
    [DisplayName("Billing Address")]
    public string? BillingAddress { get; set; }

    public string? ContactName { get; set; }

    // [FormField(EFormField.Email)]
    public string? Email { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    // [FormField(EFormField.Currency)]
    [Column(TypeName = "decimal(18,3)")]
    public decimal OpenBalance { get; set; }

    // [FormField(EFormField.Date)]
    public DateTime OpenBalanceDate { get; set; }

    // [FormField(EFormField.Phone)]
    public string? Phone { get; set; }

    // [FormField(EFormField.Lookup, "PaymentTerms")]
    public string? Terms { get; set; }

    // [FormField(EFormField.Currency)]
    [Column(TypeName = "decimal(18,3)")]
    public decimal Balance { get; set; }

    // [FormField(EFormField.Html)]
    public string? Description { get; set; }
}