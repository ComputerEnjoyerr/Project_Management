using Microsoft.AspNetCore.Mvc;
using Project_Management.Services;
using System.Security.Claims;

namespace Project_Management.Controllers
{
    public class MyTasksController : Controller
    {
        private readonly IMyTaskService _myTaskService;
        private readonly IProjectService _projectService;

        public MyTasksController(IMyTaskService myTaskService, IProjectService projectService)
        {
            _myTaskService = myTaskService;
            _projectService = projectService;
        }

        public IActionResult Index(string status, string priority, int? projectId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            var tasks = _myTaskService.GetTasksForUser(userEmail, status, priority, projectId);
            ViewBag.Projects = _projectService.GetByUser(userEmail);
            ViewBag.CurrentUser = userEmail;
            return View(tasks);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            _myTaskService.UpdateStatus(id, status);
            return RedirectToAction(nameof(Index));
        }
    }
}
