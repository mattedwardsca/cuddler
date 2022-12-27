using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuddler.Data.Entities;

[Table("Core_Settings")]
public class SettingEntity : BaseEntity, IHasContext, IHasOwner
{
    [Required]
    public string Key { get; set; } = null!;

    public string? Value { get; set; }

    public virtual AccountEntity? Owner { get; set; }

    public string? ContextId { get; set; }

    public string? ContextType { get; set; }

    [ForeignKey(nameof(Owner))]
    public string? OwnerId { get; set; }
}