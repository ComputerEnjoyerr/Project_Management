using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.Services
{
    public interface IKanbanService
    {
        KanbanViewModel GetKanbanData(int projectId, string currentUserEmail);
        bool UpdateTaskStatus(int taskId, string newStatus, string changedByEmail);
        int CreateTask(CreateTaskRequest request, string createdByEmail);
        bool CanUserAccessProject(int projectId, string userEmail);
        Objective GetTaskDetails(int taskId);
    }

    public class KanbanService : IKanbanService
    {
        private readonly ProjectManagementDbContext _context;

        public KanbanService(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public KanbanViewModel GetKanbanData(int projectId, string currentUserEmail)
        {
            // Kiểm tra quyền truy cập
            if (!CanUserAccessProject(projectId, currentUserEmail))
            {
                return null;
            }

            var project = _context.Projects
                .Include(p => p.ProjectMembers)
                .FirstOrDefault(p => p.ProjectId == projectId);
            if (project == null)
            {
                return null;
            }

            var objectives = _context.Objectives
            .Where(o => o.ProjectId == projectId)
            .Include(o => o.Comments)
            .Include(o => o.TimeEntries)
            .ToList();

            var stages = _context.Stages
                .Where(s => s.ProjectId == projectId)
                .ToList();

            var projectMembers = _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .ToList();

            return new KanbanViewModel
            {
                Project = project,
                Objectives = objectives,
                Stages = stages,
                ProjectMembers = projectMembers
            };
        }

        public bool UpdateTaskStatus(int taskId, string newStatus, string changedByEmail)
        {
            var objective = _context.Objectives.Find(taskId);
            if (objective == null)
            {
                return false;
            }

            var oldStatus = objective.Status;
            objective.Status = newStatus;

            // Ghi lại lịch sử thay đổi
            var history = new TaskHistory
            {
                TaskId = taskId,
                ChangedByUserEMail = changedByEmail,
                ChangeDate = DateTime.Now,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                Note = $"Status changed from {oldStatus} to {newStatus}"
            };

            _context.TaskHistories.Add(history);
            _context.SaveChanges();

            return true;
        }

        public int CreateTask(CreateTaskRequest request, string createdByEmail)
        {
            var objective = new Objective
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                AssignedToEmail = request.AssignedToEmail,
                CreatedByEmail = createdByEmail,
                StartDate = request.StartDate,
                DueDate = request.DueDate,
            };

            _context.Objectives.Add(objective);
            _context.SaveChanges();

            return objective.ObjectiveId;
        }

        public bool CanUserAccessProject(int projectId, string userEmail)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (project == null) return false;

            // Người tạo dự án hoặc thành viên dự án có quyền truy cập
            return project.CreatedByEmail == userEmail ||
                   _context.ProjectMembers.Any(pm => pm.ProjectId == projectId && pm.UserEmail == userEmail);
        }

        public Objective GetTaskDetails(int taskId)
        {
            var objective = _context.Objectives
                .Include(o => o.Comments)
                .Include(o => o.TimeEntries)
                .FirstOrDefault(o => o.ObjectiveId == taskId);
            if (objective == null)
            {
                return null;
            }
            return objective;
        }
    }
}
