using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Stage
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Objective { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int StatusId { get; set; }

    public int ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}
