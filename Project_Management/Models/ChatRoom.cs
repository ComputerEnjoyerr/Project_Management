using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class ChatRoom
{
    public int ChatRoomId { get; set; }

    public int ProjectId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Project Project { get; set; } = null!;
}
