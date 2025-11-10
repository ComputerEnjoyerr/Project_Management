using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class TimeEntry
{
    public int TimeEntryId { get; set; }

    public int TaskId { get; set; }

    public string? UserEmail { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? Duration { get; set; }

    public string? Note { get; set; }

    public virtual Objective Task { get; set; } = null!;

    //public virtual ApplicationUser User { get; set; } = null!;
}
