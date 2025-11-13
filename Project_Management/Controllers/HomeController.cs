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
        private readonly IProjectMemberService projectMemberService;
        private readonly IUserService userService;
        private readonly IObjectiveService objectiveService;
        public HomeController(ILogger<HomeController> logger,
            IProjectService projectService, 
            IUserService userService,
            IProjectMemberService projectMemberService,
            IObjectiveService objectiveService)
        {
            _logger = logger;
            this.projectService = projectService;
            this.userService = userService;
            this.projectMemberService = projectMemberService;
            this.objectiveService = objectiveService;
        }

        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            var userProjects = projectService
                .GetByUser(userEmail)
                .OrderByDescending(p => p.CreatedAt);
            ViewBag.Members = userService.GetUsers();
            ViewBag.CurrentUserEmail = userEmail;
            ViewBag.TotalTasks = objectiveService.GetByAssignedEmail(userEmail);
            ViewBag.TotalMembers = projectMemberService.GetTotalMembersByProjectIds(userProjects.Select(p => p.ProjectId));
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
                    projectMemberService.AddMember(project.ProjectId, project.CreatedByEmail, role: "Manager");
                }
                //modelstate.addmodelerror("enddate", "Ngày kết thúc phải sau ngày bắt đầu");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var project = projectService.GetById(id);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (project == null || userEmail == null)
            {
                return RedirectToAction("Index");
            }

            // Danh sách tài khoản
            ViewBag.Members = userService.GetUsersInProject(project.ProjectId);
            // Các tasks trong dự án
            ViewBag.ProjectTasks = objectiveService.GetByProject(project);
            // Người tạo dự án
            ViewBag.IsOwner = projectMemberService.IsProjectOwner(id, userEmail);
            // User hiện tại
            ViewBag.CurrentUser = userEmail;
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
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            if (projectMemberService.IsProjectOwner(id, userEmail))
            {
                var project = projectService.GetById(id);
                ViewBag.Members = userService.GetUsers();
                return View(project);
                
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id, string email)
        {
            if (email == null)
            {
                // Người dùng không phải thành viên dự án
                return RedirectToAction("Index");
            }
            if (projectMemberService.IsProjectOwner(id, email)) // Thành viên dự án
            {
                projectService.Delete(id);
                return RedirectToAction("Index");
            }
            else if (!projectMemberService.IsProjectOwner(id, email)) // Chủ sở hữu dự án
            {
                projectMemberService.RemoveMember(id, email);
                return RedirectToAction("Index");
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
