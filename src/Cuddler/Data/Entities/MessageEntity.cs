using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Messages")]
public class MessageEntity : BaseEntity
{
    [Required]
    public string? Body { get; set; }

    public DateTime? DateSent { get; set; }

    public DateTime? DraftDate { get; set; }

    [Required]
    [ValidateNever]
    public string[] ReceipientIds { get; set; } = null!;

    public DateTime? ScheduleSendDate { get; set; }

    [ValidateNever]
    public virtual AccountEntity Sender { get; set; } = null!;

    [Required]
    [ValidateNever]
    [ForeignKey(nameof(Sender))]
    public string SenderId { get; set; } = null!;

    public string? Status { get; set; }

    public string? Subject { get; set; }

    [Required]
    [ValidateNever]
    public string ThreadId { get; set; } = null!;
}