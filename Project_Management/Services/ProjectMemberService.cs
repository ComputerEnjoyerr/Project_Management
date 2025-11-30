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
        bool IsProjectOwner(int id, string email);
        IEnumerable<ApplicationUser> GetTotalMembersByProjectIds(IEnumerable<int> enumerable);
    }

    public class ProjectMemberService : IProjectMemberService
    {
        private readonly ProjectManagementDbContext _context;
        private readonly ApplicationDbContext _applicationDbContext;
        public ProjectMemberService(ProjectManagementDbContext context,
            ApplicationDbContext applicationDbContext)
        {
            _context = context;
            _applicationDbContext = applicationDbContext;
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

        public bool IsProjectOwner(int id, string email)
        {
            var member = _context.ProjectMembers
                .FirstOrDefault(pm => pm.ProjectId == id && pm.UserEmail == email);
            if (member != null && member.Role != "Manager")
            {
                return false; // Không phải chủ sở hữu
            }
            return true; // Là chủ sở hữu hoặc không tìm thấy thành viên
        }

        public IEnumerable<ApplicationUser> GetTotalMembersByProjectIds(IEnumerable<int> enumerable)
        {
            var projectMembers = _context.ProjectMembers
                .Where(pm => enumerable.Contains(pm.ProjectId))
                .ToList();
            var totalMembers = _applicationDbContext.Users
                .Where(u => projectMembers.Select(pm => pm.UserEmail).Contains(u.Email))
                .ToList();
            return totalMembers;
        }
    }
}
