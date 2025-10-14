using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class TimeEntry
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public string UserId { get; set; } = null!;

    public double HoursWorked { get; set; }

    public DateTime? EntryDate { get; set; }

    public virtual TaskItem Task { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
