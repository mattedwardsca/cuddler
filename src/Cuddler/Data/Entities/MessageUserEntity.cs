using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuddler.Data.Entities;

[Table("Cuddler_MessageUsers")]
public class MessageUserEntity : BaseEntity
{
    public DateTime? DateRead { get; set; }

    public bool EmailSent { get; set; }

    public virtual MessageEntity? Message { get; set; }

    [ForeignKey(nameof(Message))]
    public string? MessageId { get; set; }

    public virtual AccountEntity Receipient { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Receipient))]
    public string ReceipientId { get; set; } = null!;
}