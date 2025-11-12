using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using System;

namespace Project_Management.Services
{
   public interface IObjectiveService
    {
        IEnumerable<Objective> GetByAssignedEmail(string email, string status = null, string priority = null, int? projectId = null);
        Objective GetById(int id);
        void Create(Objective obj);
        void Update(Objective obj);
        void Delete(int id);
        bool IsProjectMember(int projectId, string email);
        bool CanEditTask(int taskId, string email);
        IEnumerable<Objective> GetByProject(Project project);
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

        public IEnumerable<Objective> GetByAssignedEmail(string email, string status = null, string priority = null, int? projectId = null)
        {
            if (string.IsNullOrEmpty(email)) return Enumerable.Empty<Objective>();

            var q = _db.Objectives.Include(o => o.Project).AsQueryable();

            q = q.Where(o => o.AssignedToEmail != null && o.AssignedToEmail.ToLower() == email.ToLower());

            if (!string.IsNullOrEmpty(status))
                q = q.Where(o => o.Status == status);
            if (!string.IsNullOrEmpty(priority))
                q = q.Where(o => o.Priority == priority);
            if (projectId.HasValue)
                q = q.Where(o => o.ProjectId == projectId.Value);

            return q.OrderByDescending(o => o.DueDate).ToList();
        }

        public Objective GetById(int id)
        {
            return _db.Objectives.Include(o => o.Project).FirstOrDefault(o => o.ObjectiveId == id);
        }

        public void Create(Objective obj)
        {
            obj.CreatedByEmail = obj.CreatedByEmail ?? throw new ArgumentNullException(nameof(obj.CreatedByEmail));
            obj.Status ??= "Todo";
            _db.Objectives.Add(obj);
            _db.SaveChanges();
        }

        public void Update(Objective obj)
        {
            var exist = _db.Objectives.Find(obj.ObjectiveId);
            if (exist == null) throw new InvalidOperationException("Objective not found");

            // update allowed fields
            exist.Title = obj.Title;
            exist.Description = obj.Description;
            exist.Priority = obj.Priority;
            exist.Status = obj.Status;
            exist.AssignedToEmail = obj.AssignedToEmail;
            exist.StartDate = obj.StartDate;
            exist.DueDate = obj.DueDate;

            if (exist.Status == "Done" && exist.CompletedAt == null)
                exist.CompletedAt = DateTime.UtcNow;
            if (exist.Status != "Done")
                exist.CompletedAt = null;

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var e = _db.Objectives.FirstOrDefault(o => o.ObjectiveId == id);
            if (e != null)
            {
                _db.Objectives.Remove(e);
                _db.SaveChanges();
            }
        }

        public bool IsProjectMember(int projectId, string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Lấy userId từ AspNetUsers
            var userId = applicationDb.Users
                .Where(u => u.Email.ToLower() == email.ToLower())
                .Select(u => u.Email)
                .FirstOrDefault();

            // Nếu userId tồn tại trong ProjectMembers hoặc là người tạo project
            bool isMember = _db.ProjectMembers.Any(pm => pm.ProjectId == projectId && pm.UserEmail == userId);
            bool isOwner = _db.Projects.Any(p => p.ProjectId == projectId && p.CreatedByEmail.ToLower() == email.ToLower());

            return isMember || isOwner;
        }


        public bool CanEditTask(int taskId, string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            var task = _db.Objectives
                .Include(o => o.Project)
                .FirstOrDefault(o => o.ObjectiveId == taskId);

            if (task == null) return false;

            // thành viên của dự án
            bool isMember = _db.ProjectMembers
                .Any(pm => pm.ProjectId == task.ProjectId && pm.UserEmail == email);

            // người tạo dự án
            bool isProjectOwner = !string.IsNullOrEmpty(task.Project?.CreatedByEmail)
                && task.Project.CreatedByEmail.ToLower() == email.ToLower();

            // người tạo task (cho chắc)
            bool isTaskCreator = !string.IsNullOrEmpty(task.CreatedByEmail)
                && task.CreatedByEmail.ToLower() == email.ToLower();

            return isMember || isProjectOwner || isTaskCreator;

        }

        public IEnumerable<Objective> GetByProject(Project project)
        {
            return _db.Objectives
                .Include (o => o.Project)
                .Where(o => o.ProjectId ==  project.ProjectId).ToList();
        }
    }
}
