using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class User : Controller
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }

        public ICollection<ProjectMember>? ProjectMembers { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
