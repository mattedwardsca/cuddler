using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Options")]
public class OptionEntity : BaseEntity, IRequiresContext
{
    [Required]
    public string Text { get; set; } = null!;

    [Required]
    public string ContextId { get; set; } = null!;

    [Required]
    public string ContextType { get; set; } = null!;
}