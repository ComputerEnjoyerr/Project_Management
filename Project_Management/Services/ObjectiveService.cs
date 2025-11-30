using Humanizer;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using System;

namespace Project_Management.Services
{
    public interface IObjectiveService
    {
        IEnumerable<Objective> GetByAssignedEmail(string userEmail);
        IEnumerable<Objective> GetByProject(Project project);
        void Add(Objective objective);
        void Update(Objective objective);
        void Delete(int id);
        Objective GetById(int id);
        bool IsAssignedTo(string userEmail, int objId);
        bool IsCreator(string userEmail, int objId);
        IEnumerable<Objective> GetTasksForUser(string userEmail, string status, string priority, int? projectId);
        void UpdateStatus(int id, string status);
    }

    public class ObjectiveService : IObjectiveService
    {
        private readonly ProjectManagementDbContext _db;
        private readonly ApplicationDbContext applicationDb;
        public ObjectiveService(ProjectManagementDbContext db,
            ApplicationDbContext applicationDb) { 
            _db = db; 
            this.applicationDb = applicationDb;
        }

        public IEnumerable<Objective> GetByAssignedEmail(string userEmail)
        {
            return _db.Objectives
                .Where(o => o.AssignedToEmail == userEmail)
                .Include(o => o.Project)
                .ToList();
        }

        public Objective GetById(int id)
        {
            var obj = _db.Objectives
                .Include(o => o.Project)
                .FirstOrDefault(o => o.ObjectiveId == id);
            return obj;
        }

        public IEnumerable<Objective> GetByProject(Project project)
        {
            return _db.Objectives
                .Where(o => o.ProjectId == project.ProjectId)
                .Include(o => o.Project)
                .ToList();
        }

        public void Add(Objective objective)
        {
            _db.Objectives.Add(objective);
            _db.SaveChanges();
        }

        public void Update(Objective objective)
        {
            _db.Objectives.Update(objective);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _db.Objectives.FirstOrDefault(o => o.ObjectiveId == id);
            if (obj != null)
            {
                _db.Remove(obj);
                _db.SaveChanges();
            }
        }
        // Nếu ko phải là ng được giao task thì ko thể xóa hoặc sửa
        public bool IsAssignedTo(string userEmail, int objId)
        {
            var obj = _db.Objectives
                .FirstOrDefault(o => o.ObjectiveId == objId &&
                                     o.AssignedToEmail == userEmail);
            if (obj != null) return true;
            return false;
        }

        // Là người tạo task hay ko
        public bool IsCreator(string userEmail, int objId)
        {
            var obj = _db.Objectives
                .FirstOrDefault(o => o.CreatedByEmail ==  userEmail &&
                                     o.ObjectiveId == objId);
            if (obj != null) return true;
            return false;
        }

        public IEnumerable<Objective> GetTasksForUser(string userEmail, string status = null, string priority = null, int? projectId = null)
        {
            var query = _db.Objectives
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
            var task = _db.Objectives.Find(taskId);
            if (task == null) return;

            task.Status = status;
            if (status == "Done") task.CompletedAt = DateTime.Now;

            _db.SaveChanges();
        }
    }
}
