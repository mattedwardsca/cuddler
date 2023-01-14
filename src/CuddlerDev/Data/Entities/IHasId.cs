using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CuddlerDev.Data.Entities;

public interface IHasId
{
    [MaxLength(36)]
    [Required]
    [ValidateNever]
    string Id { get; set; }
}