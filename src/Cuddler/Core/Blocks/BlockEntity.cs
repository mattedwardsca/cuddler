using System.ComponentModel.DataAnnotations;
using Cuddler.Core.Models;

namespace Cuddler.Core.Blocks;

public class BlockEntity : BaseEntity
{
    public string? Inputs { get; set; }

    [Required]
    public string SubmitApiUrl { get; set; } = null!;

    [Required]
    public string ContextId { get; set; } = null!;
}