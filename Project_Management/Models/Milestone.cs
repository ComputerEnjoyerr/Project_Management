using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Milestone
{
    public int MilestoneId { get; set; }

    public int ProjectId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual ICollection<Objective> Objectives { get; set; } = new List<Objective>();

    public virtual Project Project { get; set; } = null!;
}
