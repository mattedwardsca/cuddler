using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Cultures")]
public class CultureEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;

    public virtual List<ResourceEntity> Resources { get; set; } = null!;

    [Required]
    public string Symbol { get; set; } = null!;

    public string Description { get; set; } = null!;
}