using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.Services
{
    public interface IProjectService
    {
        List<Project> GetByUser(string userId);
        Project GetById(int projectId);

        void Add(Project project);
    }

    public class ProjectService : IProjectService
    {
        private readonly ProjectManagementDbContext _context;
        public ProjectService(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public List<Project> GetByUser(string userId)
        {
            return _context.Projects.Where(p => p.CreatedByEmail == userId).ToList();
        }

        public Project GetById(int project)
        {
            var proj = _context.Projects.FirstOrDefault(p => p.ProjectId == project);
            if (proj != null)
            {
                return proj;
            }
            return new Project();
        }

        public void Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }
    }
}
