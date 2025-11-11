using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using System;

namespace Project_Management.Services
{
    public interface IMyTaskService
    {
        IEnumerable<Objective> GetTasksForUser(string userEmail, string status = null, string priority = null, int? projectId = null);
        void UpdateStatus(int taskId, string status);
    }

    public class MyTaskService : IMyTaskService
    {
        private readonly ProjectManagementDbContext _context;
        public MyTaskService(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Objective> GetTasksForUser(string userEmail, string status = null, string priority = null, int? projectId = null)
        {
            var query = _context.Objectives
                .Include(o => o.Project)
                .Where(o => o.AssignedToEmail == userEmail)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);
            if (!string.IsNullOrEmpty(priority))
                query = query.Where(o => o.Priority == priority);
            if (projectId.HasValue)
                query = query.Where(o => o.ProjectId == projectId.Value);

            return query.OrderByDescending(o => o.DueDate).ToList();
        }

        public void UpdateStatus(int taskId, string status)
        {
            var task =  _context.Objectives.Find(taskId);
            if (task == null) return;

            task.Status = status;
            if (status == "Done") task.CompletedAt = DateTime.Now;

            _context.SaveChangesAsync();
        }
    }
}
