using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Notes")]
public class NoteEntity : BaseEntity, IHasDescription, IRequiresContext, IHasOwner
{
    [Required]
    [ValidateNever]
    public string CreatorId { get; set; } = null!;

    public string? Visibility { get; set; }

    [ValidateNever]
    public virtual AccountEntity Owner { get; set; } = null!;

    [Required]
    public string? Description { get; set; }

    [Required]
    [ValidateNever]
    [ForeignKey(nameof(Owner))]
    public string OwnerId { get; set; } = null!;

    [Required]
    [ValidateNever]
    public string ContextId { get; set; } = null!;

    [ValidateNever]
    [Required]
    public string ContextType { get; set; } = null!;
}