using System.ComponentModel.DataAnnotations.Schema;
using Kendo.Mvc.UI;

namespace Cuddler.Data.Entities;

[Table("BoostDC_Schedules")]
public class ScheduleEntity : BaseEntity, ISchedulerEvent
{
    public string? CalendarId { get; set; }

    public int Capacity { get; set; }

    public DateTime? DateCompleted { get; set; }

    public int? RecurrenceId { get; set; }

    public string? RegionId { get; set; }

    public string? Description { get; set; }

    public DateTime End { get; set; }

    public string? EndTimezone { get; set; }

    public bool IsAllDay { get; set; }

    public string? RecurrenceException { get; set; }

    public string? RecurrenceRule { get; set; }

    public DateTime Start { get; set; }

    public string? StartTimezone { get; set; }

    public string? Title { get; set; }
}