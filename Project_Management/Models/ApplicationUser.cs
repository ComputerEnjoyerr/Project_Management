using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management.Models;

public partial class ApplicationUser : IdentityUser
{

    //public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [NotMapped]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    [NotMapped]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    [NotMapped]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    [NotMapped]
    public virtual ICollection<Objective> ObjectiveAssignedToNavigations { get; set; } = new List<Objective>();
    [NotMapped]
    public virtual ICollection<Objective> ObjectiveCreatedByNavigations { get; set; } = new List<Objective>();
    [NotMapped]
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    [NotMapped]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    [NotMapped]
    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
    [NotMapped]
    public virtual ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
