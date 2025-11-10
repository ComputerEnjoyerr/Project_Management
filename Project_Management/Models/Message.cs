using System;
using System.Collections.Generic;

namespace Project_Management.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int ChatRoomId { get; set; }

    public string? SenderEmail { get; set; }

    public string? Content { get; set; }

    public DateTime? SentAt { get; set; }

    public virtual ChatRoom ChatRoom { get; set; } = null!;

    //public virtual ApplicationUser Sender { get; set; } = null!;
}
