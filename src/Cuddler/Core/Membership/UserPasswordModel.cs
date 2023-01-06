using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Cuddler.Core.Membership;

public class UserPasswordModel
{
    [Required]
    [HiddenInput]
    public string AccountId { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; } = null!;
}