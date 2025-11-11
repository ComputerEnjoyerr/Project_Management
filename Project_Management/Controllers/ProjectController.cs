using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using System.Security.Claims;

namespace Project_Management.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectManagementDbContext _context;

        public ProjectController(ProjectManagementDbContext context)
        {
            _context = context;
        }
        // Trang danh sách các dự án (chưa xử lý ở đây)
        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
                return Redirect("~/Identity/Account/Login");

            return View();
        }

        // 👇 Trang chi tiết 1 dự án — hiển thị Kanban
        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Objectives)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            // Tách task theo trạng thái
            ViewBag.Todo = project.Objectives.Where(o => o.Status == "Todo").ToList();
            ViewBag.InProgress = project.Objectives.Where(o => o.Status == "In Progress").ToList();
            ViewBag.Review = project.Objectives.Where(o => o.Status == "Review").ToList();
            ViewBag.Done = project.Objectives.Where(o => o.Status == "Done").ToList();

            return View(project);
        }
    }
}
