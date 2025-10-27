using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Methodology { get; set; }

    public string? Status { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();

    public virtual ICollection<Objective> Objectives { get; set; } = new List<Objective>();

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    public virtual ICollection<Stage> Stages { get; set; } = new List<Stage>();
}
