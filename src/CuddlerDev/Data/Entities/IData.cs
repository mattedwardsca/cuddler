using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CuddlerDev.Data.Entities;

public interface IData : IHasId
{
    DateTime? DateArchived { get; set; }

    [Required]
    [ValidateNever]
    DateTime DateCreated { get; set; }

    [Required]
    [ValidateNever]
    DateTime DateUpdated { get; set; }
}