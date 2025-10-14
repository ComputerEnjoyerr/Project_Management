using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int PiorityId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? Deadline { get; set; }

    public int StageId { get; set; }

    public int ProjectId { get; set; }

    public int StatusId { get; set; }

    public string TaskToUserId { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Piority Piority { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual Stage Stage { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();

    public virtual AspNetUser TaskToUser { get; set; } = null!;

    public virtual ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
