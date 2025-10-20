using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class Project : Controller
    {
        public int ProjectID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<ProjectMember>? ProjectMembers { get; set; }
        public ICollection<Stage>? Stages { get; set; }
    }
}
