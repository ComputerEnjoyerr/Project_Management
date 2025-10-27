using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class TaskHistory
{
    public int HistoryId { get; set; }

    public int TaskId { get; set; }

    public int ChangedByUserId { get; set; }

    public DateTime? ChangeDate { get; set; }

    public string? OldStatus { get; set; }

    public string? NewStatus { get; set; }

    public string? Note { get; set; }

    public virtual User ChangedByUser { get; set; } = null!;

    public virtual Objective Task { get; set; } = null!;
}
