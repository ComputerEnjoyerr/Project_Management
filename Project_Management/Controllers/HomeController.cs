using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management.Models;
using Project_Management.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace Project_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectService projectService;
        private readonly IUserService userService;
        private readonly IKanbanService kanbanService;
        private readonly IDashboardService dashboardService;
        public HomeController(ILogger<HomeController> logger, IProjectService projectService, IUserService userService, IKanbanService kanbanService, IDashboardService dashboardService)
        {
            _logger = logger;
            this.projectService = projectService;
            this.userService = userService;
            this.kanbanService = kanbanService;
            this.dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            var userProjects = projectService.GetByUser(userEmail);
            ViewBag.Members = userService.GetUsers();
            ViewBag.CurrentUserEmail = userEmail;
            ViewBag.KanbanPreviews = dashboardService.GetDashboardData(userEmail);
            return View(userProjects);
        }

        // Tạo dự án mới
        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra ngày bắt đầu và kết thúc có phù hợp
                if (project.IsDateValid())
                {
                    // Dữ liệu thõa mãn các điều kiện
                    project.Status = "Planning";
                    project.CreatedAt = DateTime.Now;
                    projectService.Add(project);
                }
                //modelstate.addmodelerror("enddate", "Ngày kết thúc phải sau ngày bắt đầu");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var project = projectService.GetById(id);
            ViewBag.Members = userService.GetUsers();
            return View(project);
        }

        [HttpPost]
        public IActionResult Update(Project project)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra ngày bắt đầu và kết thúc có phù hợp
                if (project.IsDateValid())
                {
                    projectService.Update(project);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Update", new { id = project.ProjectId });
        }

        public IActionResult Update(int id)
        {
            var project = projectService.GetById(id);
            ViewBag.Members = userService.GetUsers();
            return View(project);
        }

        public IActionResult Delete(int id)
        {
            projectService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetKanbanPreview(int projectId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            var kanbanPreview = dashboardService.GetKanbanPreview(projectId, userEmail);

            return PartialView("_KanbanPreviewPartial", kanbanPreview);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
