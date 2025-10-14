using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Piority
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Level { get; set; }

    public virtual ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}
