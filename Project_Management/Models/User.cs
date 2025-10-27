using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Objective> ObjectiveAssignedToNavigations { get; set; } = new List<Objective>();

    public virtual ICollection<Objective> ObjectiveCreatedByNavigations { get; set; } = new List<Objective>();

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();

    public virtual ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
