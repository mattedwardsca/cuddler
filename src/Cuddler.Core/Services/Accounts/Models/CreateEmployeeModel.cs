using System.ComponentModel.DataAnnotations;

namespace Cuddler.Core.Services.Accounts.Models;

public class CreateEmployeeModel
{
    public string? AccountNumber { get; set; }

    public string? Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    public bool IsAdvisor { get; set; }

    public bool IsAuditor { get; set; }

    public bool IsCoordinator { get; set; }

    public bool UnlockAccount { get; set; }

    public string LastName { get; set; } = null!;

    [Phone]
    public string? PhoneNumber { get; set; }
}