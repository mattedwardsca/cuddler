using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuddler.Data.Entities;

[Table("Cuddler_EmailTemplates")]
public class EmailTemplateEntity : BaseEntity
{
    [Required]
    public string Body { get; set; } = null!;

    [Required]
    public string? Category { get; set; }

    public string? Description { get; set; }

    [Required]
    public string Purpose { get; set; } = null!;

    [Required]
    public string Subject { get; set; } = null!;
}