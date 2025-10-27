using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class ProjectMember
{
    public int ProjectMemberId { get; set; }

    public int ProjectId { get; set; }

    public int UserId { get; set; }

    public string? Role { get; set; }

    public DateTime? JoinedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
