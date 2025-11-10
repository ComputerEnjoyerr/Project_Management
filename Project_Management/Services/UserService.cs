using Project_Management.Data;
using Project_Management.Data.Migrations;
using Project_Management.Models;

namespace Project_Management.Services
{
    public interface IUserService
    {
        List<ApplicationUser> GetUsers();
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
    }
}
