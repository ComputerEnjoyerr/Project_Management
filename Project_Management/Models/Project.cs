using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string OwnerId { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public virtual AspNetUser Owner { get; set; } = null!;

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Stage> Stages { get; set; } = new List<Stage>();

    public virtual ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}
