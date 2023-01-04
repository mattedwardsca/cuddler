using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Cuddler_TransactionCategories")]
public class TransactionCategoryEntity : BaseEntity, IHasBalance, IHasParent, IHasDescription
{
    //[UIHint("ExpenseCategoryTypesDropdown")]
    public string? CategoryType { get; set; }

    [Required]
    [ValidateNever]
    public string Name { get; set; } = null!;

    public virtual TransactionCategoryEntity? Parent { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Balance { get; set; }

    public string? Description { get; set; }

    [ForeignKey(nameof(Parent))]
    public string? ParentId { get; set; }

    public bool IsArchived()
    {
        return DateArchived != null;
    }

    public override string ToString()
    {
        return Name;
    }
}