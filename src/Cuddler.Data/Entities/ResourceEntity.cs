using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Resources")]
public class ResourceEntity
{
    public string CultureId { get; set; } = null!;

    [Required]
    public string Key { get; set; } = null!;

    [Required]
    public string Value { get; set; } = null!;

    

    public DateTime? DateArchived { get; set; }

    [Required]
    [ValidateNever]
    public DateTime DateCreated { get; set; }

    [Required]
    [ValidateNever]
    public DateTime DateUpdated { get; set; }

    [JsonInclude]
    [MaxLength(36)]
    [Required]
    [ValidateNever]
    [HiddenInput]
    public string Id { get; set; } = null!;

    [JsonIgnore]
    [MaxLength(36)]
    [ValidateNever]
    public string? PolicyTagId { get; set; }
}