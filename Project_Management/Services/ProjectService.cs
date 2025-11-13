using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.Services
{
    public interface IProjectService
    {
        List<Project> GetByUser(string userId);
        Project GetById(int projectId);

        void Add(Project project);
        void Delete(int projectId);
        void Update(Project project);
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
            var userProjects = _context.Projects
                .Where(p => p.CreatedByEmail == userId)
                .ToList();

            var memberProjects = _context.ProjectMembers
                .Where(pm => pm.UserEmail == userId)
                .Select(pm => pm.Project)
                .ToList();

            var allProjects = userProjects
                .UnionBy(memberProjects, p => p.ProjectId) // loại bỏ trùng theo ProjectId
                .ToList();

            return allProjects;
        }

        public Project GetById(int project)
        {
            var proj = _context.Projects
                .Include(p => p.Objectives)
                .FirstOrDefault(p => p.ProjectId == project);
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

        public void Delete(int projectId)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }

        public void Update(Project project)
        {
            var existing = _context.Projects.FirstOrDefault(p => p.ProjectId == project.ProjectId);
            if (existing != null)
            {
                existing.Name = project.Name;
                existing.Description = project.Description;
                existing.StartDate = project.StartDate;
                existing.EndDate = project.EndDate;
                existing.Status = project.Status;
                existing.Methodology = project.Methodology;
                existing.CreatedByEmail = project.CreatedByEmail;
                existing.CompletedAt = project.CompletedAt;

            }
            _context.SaveChanges();
        }
    }
}
