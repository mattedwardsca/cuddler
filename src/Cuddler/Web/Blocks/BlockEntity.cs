using System.ComponentModel.DataAnnotations;
using Cuddler.Data.Entities;

namespace Cuddler.Web.Blocks;

public class BlockEntity : BaseEntity
{
    public string? Inputs { get; set; }

    [Required]
    public string SubmitApiUrl { get; set; } = null!;

    [Required]
    public string ContextId { get; set; } = null!;
}