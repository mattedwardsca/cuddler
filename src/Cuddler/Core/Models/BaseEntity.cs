using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Core.Models;

public abstract class BaseEntity : IData
{
    public string? ArchiveReason { get; set; }

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