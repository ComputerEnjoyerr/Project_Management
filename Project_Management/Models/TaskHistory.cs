using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class TaskHistory
{
    public int Id { get; set; }

    public string OldValue { get; set; } = null!;

    public string NewValue { get; set; } = null!;

    public DateTime? ChangeAt { get; set; }

    public int TaskId { get; set; }

    public string ChangedByUserId { get; set; } = null!;

    public virtual AspNetUser ChangedByUser { get; set; } = null!;

    public virtual TaskItem Task { get; set; } = null!;
}
