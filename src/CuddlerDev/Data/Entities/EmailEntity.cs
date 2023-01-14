using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuddlerDev.Data.Entities;

[Table("Cuddler_Emails")]
public class EmailEntity : BaseEntity
{
    public string? BccEmail { get; set; }

    public string? CcEmail { get; set; }

    public DateTime? DateLastFailed { get; set; }

    public DateTime? DateSentSucceeded { get; set; }

    public string? EmailTemplateId { get; set; }

    public string? FailedReason { get; set; }

    public bool FailedTryAgain { get; set; }

    [Required]
    public string? FromEmail { get; set; }

    public string? Message { get; set; }

    public string? SendResponseText { get; set; }

    public int SendStatusCode { get; set; }

    public bool SendSuccess { get; set; }

    [Required]
    public string? Subject { get; set; }

    [Required]
    public string? ToEmail { get; set; }

    public string? ToName { get; set; }
}