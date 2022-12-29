using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Core_Lookups")]
public class FieldEntity : BaseEntity, IHasOwner, IRequiresContext
{
    [Required]
    [ValidateNever]
    public string DataType { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    [ValidateNever]
    public virtual AccountEntity Owner { get; set; } = null!;

    [Required]
    [ValidateNever]
    [ForeignKey(nameof(Owner))]
    public string OwnerId { get; set; } = null!;

    [Required]
    [ValidateNever]
    public string ContextId { get; set; } = null!;

    [Required]
    [ValidateNever]
    public string ContextType { get; set; } = null!;

    public override string ToString()
    {
        return $"{ContextType} > {Name}";
    }
}