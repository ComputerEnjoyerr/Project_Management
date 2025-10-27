using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Sprint
{
    public int SprintId { get; set; }

    public int ProjectId { get; set; }

    public string? Name { get; set; }

    public string? Goal { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Objective> Objectives { get; set; } = new List<Objective>();

    public virtual Project Project { get; set; } = null!;
}
