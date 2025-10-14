using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string Message { get; set; } = null!;

    public int? TaskId { get; set; }

    public string UserId { get; set; } = null!;

    public int NotificationTypeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsRead { get; set; }

    public virtual NotificationType NotificationType { get; set; } = null!;

    public virtual TaskItem? Task { get; set; }

    public virtual AspNetUser User { get; set; } = null!;
}
