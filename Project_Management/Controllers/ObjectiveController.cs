using Microsoft.AspNetCore.Mvc;
using Project_Management.Models;
using Project_Management.Services;
using System.Security.Claims;

namespace Project_Management.Controllers
{
    public class ObjectiveController : Controller
    {
        private readonly IObjectiveService _service;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        public ObjectiveController(IObjectiveService service, IProjectService projectService, IUserService userService)
        {
            _service = service;
            _projectService = projectService;
            _userService = userService;
        }

        public IActionResult Index(string status, string priority, int? projectId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            ViewBag.Projects = _projectService.GetByUser(userEmail);
            ViewBag.CurrentUser = userEmail;
            return View();
        }
        public IActionResult Detail(string id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            var obj = _service.GetByAssignedEmail(id);
            ViewBag.Members = _userService.GetUsers();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTaskFromProjectDetail(ObjectiveCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Project", new { id = vm.ProjectId });
            }

            var objective = new Objective
            {
                ProjectId = vm.ProjectId,
                Title = vm.Title,
                Description = vm.Description,
                Priority = vm.Priority,
                Status = vm.Status,
                AssignedToEmail = vm.AssignedToEmail,
                StartDate = vm.StartDate.HasValue ? DateOnly.FromDateTime(vm.StartDate.Value) : DateOnly.FromDateTime(DateTime.Now),
                DueDate = vm.DueDate.HasValue ? DateOnly.FromDateTime(vm.DueDate.Value) : null,
                CreatedByEmail = User.FindFirstValue(ClaimTypes.Email)
            };

            _service.Add(objective);
            return RedirectToAction("Detail", "Home", new { id = vm.ProjectId });
        }
    }
}
