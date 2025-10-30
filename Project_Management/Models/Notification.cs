using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? UserId { get; set; }

    public string? Type { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
}
