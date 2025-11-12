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

        // ---------- INDEX (MyTasks style) ----------
        // GET: /Objectives?status=&priority=&projectId=
        public IActionResult Index(string status, string priority, int? projectId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            var tasks = _service.GetByAssignedEmail(userEmail, status, priority, projectId);
            ViewBag.Projects = _projectService.GetByUser(userEmail);
            ViewBag.CurrentUser = userEmail;
            return View(tasks);
        }

        // ---------- DETAILS ----------
        public IActionResult Details(int id)
        {
            var task = _service.GetById(id);
            if (task == null) return NotFound();
            // Optionally restrict: only assigned user or project member can view
            return View(task);
        }

        // ---------- CREATE ----------
        public IActionResult Create(int projectId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            // check membership
            if (!_service.IsProjectMember(projectId, userEmail))
                return Forbid();

            ViewBag.ProjectId = projectId;
            ViewBag.CurrentUser = userEmail;
            ViewBag.Members = _userService.GetUsers();
            return View(new Objective { ProjectId = projectId, CreatedByEmail = userEmail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Objective model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            if (!_service.IsProjectMember(model.ProjectId, userEmail))
                return Forbid();

            if (!ModelState.IsValid) return View(model);

            model.CreatedByEmail = userEmail;
            _service.Create(model);
            TempData["success"] = "Tạo công việc thành công";
            return RedirectToAction("Index", "MyTasks");
        }

        // ---------- EDIT ----------
        public IActionResult Edit(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            var task = _service.GetById(id);
            if (task == null) return NotFound();

            //if (!_service.CanEditTask(id, userEmail))
            //    return Forbid();

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Objective model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            //if (!_service.CanEditTask(model.ObjectiveId, userEmail))
            //    return Forbid();

            //if (!ModelState.IsValid) return View(model);

            _service.Update(model);
            TempData["success"] = "Cập nhật công việc thành công";
            return RedirectToAction("Index", "MyTasks");
        }

        // ---------- DELETE ----------
        public IActionResult Delete(int id)
        {
            //var userEmail = User.FindFirstValue(ClaimTypes.Email);
            //if (userEmail == null)
            //{
            //    return Redirect("~/Identity/Account/Login");
            //}

            //var task = _service.GetById(id);
            //if (task == null) return NotFound();
            ////if (!_service.CanEditTask(id, userEmail))
            ////    return Forbid();

            //return View(task);

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            //if (!_service.CanEditTask(id, userEmail))
            //    return Forbid();

            _service.Delete(id);
            TempData["success"] = "Xóa công việc thành công";
            return RedirectToAction("Index", "MyTasks");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            //if (!_service.CanEditTask(id, userEmail))
            //    return Forbid();

            _service.Delete(id);
            TempData["success"] = "Xóa công việc thành công";
            return RedirectToAction("Index", "MyTasks");
        }
    }
}
