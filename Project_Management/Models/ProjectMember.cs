using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class ProjectMember : Controller
    {
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }

        public Project? Project { get; set; }
        public User? User { get; set; }
    }
}
