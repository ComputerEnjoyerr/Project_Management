using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Stage> Stages { get; set; } = new List<Stage>();

    public virtual ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}
