using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int TaskId { get; set; }

    public int UserId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Objective Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
