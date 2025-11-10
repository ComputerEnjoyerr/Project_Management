using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.Services
{

    public interface IProjectMemberService
    {
        IEnumerable<ProjectMember> GetMembersByProject(int projectId);
        void AddMember(int projectId, string userEmail, string role);
        void RemoveMember(int projectId, string userEmail);
        bool IsUserInProject(int projectId, string userEmail);
    }

    public class ProjectMemberService : IProjectMemberService
    {
        private readonly ProjectManagementDbContext _context;
        public ProjectMemberService(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProjectMember> GetMembersByProject(int projectId)
        {
            return _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .ToList();
        }

        public void AddMember(int projectId, string userEmail, string role)
        {
            // Tránh thêm trùng
            if (IsUserInProject(projectId, userEmail)) return;

            var member = new ProjectMember
            {
                ProjectId = projectId,
                UserEmail = userEmail,
                Role = role
            };

            _context.ProjectMembers.Add(member);
            _context.SaveChanges();
        }

        public void RemoveMember(int projectId, string userEmail)
        {
            var member = _context.ProjectMembers
                .FirstOrDefault(pm => pm.ProjectId == projectId && pm.UserEmail == userEmail);
            if (member != null)
            {
                _context.ProjectMembers.Remove(member);
                _context.SaveChanges();
            }
        }

        public bool IsUserInProject(int projectId, string userEmail)
        {
            return _context.ProjectMembers
                .Any(pm => pm.ProjectId == projectId && pm.UserEmail == userEmail);
        }
    }
}
