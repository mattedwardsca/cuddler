using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Fields")]
public class FieldEntity : BaseEntity, IHasOwner
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
    public string Category { get; set; } = null!;

    public override string ToString()
    {
        return $"{Category} > {Name}";
    }
}