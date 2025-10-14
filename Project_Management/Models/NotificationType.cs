using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class NotificationType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
