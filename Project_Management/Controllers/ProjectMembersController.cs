using Microsoft.AspNetCore.Mvc;
using Project_Management.Services;

namespace Project_Management.Controllers
{
    public class ProjectMembersController : Controller
    {
        private readonly IProjectMemberService _projectMemberService;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public ProjectMembersController(
            IProjectMemberService projectMemberService,
            IUserService userService,
            IProjectService projectService)
        {
            _projectMemberService = projectMemberService;
            _userService = userService;
            _projectService = projectService;
        }

        // Danh sách thành viên trong dự án
        public  IActionResult Index(int projectId)
        {
            var project = _projectService.GetById(projectId);
            if (project == null) return NotFound();

            ViewBag.Project = project;
            ViewBag.Members = _userService.GetUsers();
            var members = _projectMemberService.GetMembersByProject(projectId);
            return View(members);
        }

        // Thêm nhân viên
        [HttpPost]
        public IActionResult Add(int projectId, string userId, string role = "Member")
        {
            _projectMemberService.AddMember(projectId, userId, role);
            return RedirectToAction("Index", new { projectId });
        }

        // Xóa nhân viên
        [HttpPost]
        public IActionResult Remove(int projectId, string userId)
        {
            _projectMemberService.RemoveMember(projectId, userId);
            return RedirectToAction("Index", new { projectId });
        }
    }
}
