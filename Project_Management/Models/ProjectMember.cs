using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class ProjectMember
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string UserId { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? JoinedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
