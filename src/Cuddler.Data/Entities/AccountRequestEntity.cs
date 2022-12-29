using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Cuddler_AccountRequests")]
public class AccountRequestEntity : BaseEntity
{
    [ForeignKey(nameof(AccountId))]
    public AccountEntity? Account { get; set; }

    public string? AccountId { get; set; }

    public DateTime? DateApproved { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    [ValidateNever]
    public DateTime ExpiryDate { get; set; }

    public bool IsAdmin { get; set; }

    public string Name { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTime? DateCompleted { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public virtual OrganizationEntity Organization { get; set; } = null!;

    [Required]
    [ValidateNever]
    [ForeignKey(nameof(Organization))]
    public string OrganizationId { get; set; } = null!;

    public bool IsReadOnly()
    {
        return AccountId != null;
    }
}