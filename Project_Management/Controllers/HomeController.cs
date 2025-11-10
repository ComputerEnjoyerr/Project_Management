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
        public HomeController(ILogger<HomeController> logger, IProjectService projectService, IUserService userService)
        {
            _logger = logger;
            this.projectService = projectService;
            this.userService = userService;
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
