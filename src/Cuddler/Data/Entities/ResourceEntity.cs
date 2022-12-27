using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuddler.Data.Entities;

[Table("Core_Resources")]
public class ResourceEntity : BaseEntity
{
    public string CultureId { get; set; } = null!;

    [Required]
    public string Key { get; set; } = null!;

    [Required]
    public string Value { get; set; } = null!;
}