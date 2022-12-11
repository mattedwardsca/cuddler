using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data;

public interface IData : IHasId
{
    string? ArchiveReason { get; set; }

    DateTime? DateArchived { get; set; }

    [Required]
    [ValidateNever]
    DateTime DateCreated { get; set; }

    [Required]
    [ValidateNever]
    DateTime DateUpdated { get; set; }

    [JsonIgnore]
    [ValidateNever]
    string? PolicyTagId { get; set; }
}