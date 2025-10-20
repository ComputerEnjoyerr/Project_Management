using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class Notification : Controller
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int? RelatedTaskID { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
