using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.Services
{
    // Services/IDashboardService.cs
    public interface IDashboardService
    {
        DashboardViewModel GetDashboardData(string currentUserEmail);
        KanbanViewModel GetKanbanPreview(int projectId, string currentUserEmail);
    }

    // Services/DashboardService.cs
    public class DashboardService : IDashboardService
    {
        private readonly ProjectManagementDbContext _context;
        private readonly IKanbanService _kanbanService;

        public DashboardService(ProjectManagementDbContext context, IKanbanService kanbanService)
        {
            _context = context;
            _kanbanService = kanbanService;
        }

        public DashboardViewModel GetDashboardData(string currentUserEmail)
        {
            // Lấy danh sách dự án user có quyền truy cập
            var accessibleProjects = _context.Projects
                .Where(p => p.CreatedByEmail == currentUserEmail ||
                           _context.ProjectMembers.Any(pm => pm.ProjectId == p.ProjectId && pm.UserEmail == currentUserEmail))
                .Include(p => p.Objectives)
                .Include(p => p.ProjectMembers)
                .ToList();

            // Tính toán thống kê
            var totalTasks = accessibleProjects.Sum(p => p.Objectives.Count);
            var totalMembers = accessibleProjects.SelectMany(p => p.ProjectMembers).Count();
            var averageProgress = CalculateAverageProgress(accessibleProjects);

            // Lấy Kanban preview cho project đầu tiên (hoặc project được chọn)
            var previewProject = accessibleProjects.FirstOrDefault();
            var kanbanPreview = previewProject != null ?
                GetKanbanPreview(previewProject.ProjectId, currentUserEmail) : null;
            return new DashboardViewModel
            {
                Projects = accessibleProjects,
                KanbanPreview = kanbanPreview,
                TotalTasks = totalTasks,
                TotalMembers = totalMembers,
                AverageProgress = averageProgress
            };
        }

        public KanbanViewModel GetKanbanPreview(int projectId, string currentUserEmail)
        {
            return _kanbanService.GetKanbanData(projectId, currentUserEmail);
        }

        private double CalculateAverageProgress(List<Project> projects)
        {
            if (!projects.Any()) return 0;

            // Logic tính tiến độ trung bình - bạn có thể tùy chỉnh
            var totalProgress = 0.0;
            var count = 0;

            foreach (var project in projects)
            {
                var completedTasks = project.Objectives.Count(o => o.Status == "Done" || o.Status == "Completed");
                var totalTasks = project.Objectives.Count;

                if (totalTasks > 0)
                {
                    totalProgress += (double)completedTasks / totalTasks * 100;
                    count++;
                }
            }

            return count > 0 ? Math.Round(totalProgress / count, 1) : 0;
        }
    }
}
