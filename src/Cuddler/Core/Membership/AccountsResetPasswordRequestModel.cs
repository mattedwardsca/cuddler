using System.ComponentModel.DataAnnotations;

namespace Cuddler.Core.Membership;

public class AccountsResetPasswordRequestModel
{
    [Required]
    public string CallbackUrl { get; set; } = null!;

    [Required]
    public string Domain { get; set; } = null!;

    [Required]
    public string? Email { get; set; } = null!;

    public string? EmailTemplate { get; set; }

    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;
}