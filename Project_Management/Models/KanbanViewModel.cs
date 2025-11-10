namespace Project_Management.Models
{
    public class KanbanViewModel
    {
        public Project Project { get; set; } = null!;
        public List<Objective> Objectives { get; set; } = null!;
        public List<Stage> Stages { get; set; } = null!;
        public List<ProjectMember> ProjectMembers { get; set; } = null!;
    }

    public class CreateTaskRequest
    {
        public int ProjectId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public string AssignedToEmail { get; set; } = null!;
        public DateOnly? StartDate { get; set; }
        public DateOnly? DueDate { get; set; }
    }

    public class UpdateTaskStatusRequest
    {
        public int TaskId { get; set; }
        public string? NewStatus { get; set; }
    }

    public class DashboardViewModel
    {
        public List<Project> Projects { get; set; } = null!;
        public KanbanViewModel KanbanPreview { get; set; } = null!;
        public int TotalTasks { get; set; }
        public int TotalMembers { get; set; }
        public double AverageProgress { get; set; }
    }
}
