using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class TaskHistory : Controller
    {
        public int HistoryID { get; set; }
        public int TaskID { get; set; }
        public int ChangedByUserID { get; set; }
        public string ChangeType { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
