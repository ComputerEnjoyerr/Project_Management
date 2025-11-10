using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Objective
{
    public int ObjectiveId { get; set; }

    public int ProjectId { get; set; }

    public int? SprintId { get; set; }

    public int? MilestoneId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Priority { get; set; }

    public string? Status { get; set; }

    public string? AssignedToEmail { get; set; }

    public string? CreatedByEmail { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    //public virtual ApplicationUser? AssignedToNavigation { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    //public virtual ApplicationUser CreatedByNavigation { get; set; } = null!;

    public virtual Milestone? Milestone { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Sprint? Sprint { get; set; }

    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();

    public virtual ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
