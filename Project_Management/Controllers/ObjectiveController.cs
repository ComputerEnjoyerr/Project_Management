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
            return View();
        }

        public IActionResult Detail(string id)
        {
            return View();
        }
       
    }
}
