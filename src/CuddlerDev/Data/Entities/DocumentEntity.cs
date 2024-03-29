using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CuddlerDev.Data.Entities;

[Table("Cuddler_Documents")]
public class DocumentEntity : BaseEntity, IRequiresContext, IHasOwner
{
    public string? ClientContextId { get; set; }

    public string? ClientId { get; set; }

    public string? Description { get; set; }

    [StringLength(20)]
    public string? Extension { get; set; }

    public string? FolderId { get; set; }

    public string? MimeType { get; set; }

    [StringLength(200)]
    public string? Tags { get; set; }

    [StringLength(100)]
    public string? Title { get; set; }

    public virtual AccountEntity Owner { get; set; } = null!;

    [Required]
    [ValidateNever]
    [ForeignKey(nameof(Owner))]
    public string OwnerId { get; set; } = null!;

    [Required]
    public string ContextId { get; set; } = null!;

    [Required]
    [ValidateNever]
    public string ContextType { get; set; } = null!;

    public string GetPathToFileDirectory(string rootFolder)
    {
        return $@"{rootFolder}\Uploads\{Id}";
    }

    public string GetPathToFile(string rootFolder)
    {
        return $@"{rootFolder}\Uploads\{Id}\{Id}.{Extension}";
    }

    public string GetPathToThumbnail(string rootFolder, int w)
    {
        return $@"{rootFolder}\Uploads\{Id}\{w}.{Extension}";
    }
}