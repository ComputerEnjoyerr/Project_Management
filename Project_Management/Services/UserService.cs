using Project_Management.Data;
using Project_Management.Data.Migrations;
using Project_Management.Models;

namespace Project_Management.Services
{
    public interface IUserService
    {
        List<ApplicationUser> GetUsers();
        List<ApplicationUser> GetUsersInProject(int projectId);
    }

    public class UserService : IUserService
    {
        private readonly ProjectManagementDbContext _context;
        private readonly ApplicationDbContext _applicationDbContext;
        public UserService(ProjectManagementDbContext context, ApplicationDbContext applicationDbContext)
        {
            _context = context;
            _applicationDbContext = applicationDbContext;
        }

        public List<ApplicationUser> GetUsers()
        {
            return _applicationDbContext.Users.ToList();
        }

        public List<ApplicationUser> GetUsersInProject(int projectId)
        {
            var userEmails = _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Select(pm => pm.UserEmail)
                .ToList();
            return _applicationDbContext.Users
                .Where(u => userEmails.Contains(u.Email))
                .ToList();
        }
    }
}
