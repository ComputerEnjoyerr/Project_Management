using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int TaskId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual TaskItem Task { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
