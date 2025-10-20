using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class TaskItem : Controller
    {
        public int TaskID { get; set; }
        public int ProjectID { get; set; }
        public int StageID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public int AssignedUserID { get; set; }
        public int CreatedUserID { get; set; }
    }
}
