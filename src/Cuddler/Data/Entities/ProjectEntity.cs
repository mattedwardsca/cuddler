using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuddler.Data.Entities;

[Table("Cuddler_Projects")]
public class ProjectEntity : BaseEntity, IHasName, IHasToken, ISortable, ICard
{
    public string? Background { get; set; }

    //public string? BaseUrl { get; set; }

    public virtual AccountEntity Client { get; set; } = null!;

    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; } = null!;

    public string? AssigneeId { get; set; }

    // ReSharper disable once Mvc.TemplateNotResolved
    [UIHint("ClientsStatus")]
    public string? ClientsStatus { get; set; }

    public DateTime? DateCompleted { get; set; }

    public DateTime? DateInitiated { get; set; }

    public DateTime? DatePlanned { get; set; }

    public DateTime? DateWipped { get; set; }

    public DateTime? EndDate { get; set; }

    //public string? ExecutiveSummary { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Gst { get; set; }

    public bool HasSalesTax { get; set; }

    public string? LeadId { get; set; }

    public string? NextStep { get; set; }

    // ReSharper disable once Mvc.TemplateNotResolved
    [UIHint("Date")]
    public DateTime? PlanningToBuyDate { get; set; }

    [Required]
    public string ProjectTypeId { get; set; } = null!;

    //public string? Purpose { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Quantity { get; set; }

    public DateTime? StartDate { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Total { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal UnitPrice { get; set; }

    public bool VisibleToClient { get; set; }

    public DateTime? DateWon { get; set; }

    public DateTime? DateLost { get; set; }

    public string? Description { get; set; }

    public string? BoardId { get; set; }

    public string? Status { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Token { get; set; } = null!;

    public int SortOrder { get; set; }

    public string GetProjectStatus()
    {
        var status = "Pending";
        if (DateInitiated != null)
        {
            status = "Submitted";
        }

        if (DateCompleted != null)
        {
            status = "Completed";
        }

        if (DateArchived != null)
        {
            status = "Deleted";
        }

        return status;
    }

    public override string ToString()
    {
        return $"{Token}";
    }

    public bool IsReadOnly()
    {
        return DateArchived != null || DateWon != null || DateLost != null;
    }
}