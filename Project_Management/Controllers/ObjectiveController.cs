using Microsoft.AspNetCore.Mvc;
using Project_Management.Models;
using Project_Management.Services;
using System.Security.Claims;
using System.Text.Json;

namespace Project_Management.Controllers
{
    public class ObjectiveController : Controller
    {
        private readonly IObjectiveService _objectiveService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        public ObjectiveController(IObjectiveService service, IProjectService projectService, IUserService userService)
        {
            _objectiveService = service;
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
            var tasks = _objectiveService.GetTasksForUser(userEmail, status, priority, projectId);
            ViewBag.Projects = _projectService.GetByUser(userEmail);
            ViewBag.CurrentUser = userEmail;
            return View(tasks);
        }

        public IActionResult Detail(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            var obj = _objectiveService.GetById(id);
            //ViewBag.Members = _userService.GetUsers();
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

            _objectiveService.Add(objective);
            return RedirectToAction("Detail", "Home", new { id = vm.ProjectId });
        }

        public IActionResult Update(int id)
        {
            var obj = _objectiveService.GetById(id);
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.Members = _userService.GetUsersInProject(obj.ProjectId);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ObjectiveUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var existingObjective = _objectiveService.GetById(vm.ObjectiveId);
                if (existingObjective == null)
                {
                    return NotFound();
                }
                existingObjective.Title = vm.Title;
                existingObjective.Description = vm.Description;
                existingObjective.Priority = vm.Priority;
                existingObjective.Status = vm.Status;
                existingObjective.AssignedToEmail = vm.AssignedToEmail;
                existingObjective.StartDate = vm.StartDate.HasValue ? DateOnly.FromDateTime(vm.StartDate.Value) : DateOnly.FromDateTime(DateTime.Now);
                existingObjective.DueDate = vm.DueDate.HasValue ? DateOnly.FromDateTime(vm.DueDate.Value) : null;
                _objectiveService.Update(existingObjective);
            }
            return RedirectToAction("Detail", "Objective", new { id = vm.ObjectiveId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = _objectiveService.GetById(id);
            if (obj == null)
            {
                return NotFound();
            }
            _objectiveService.Delete(id);
            return RedirectToAction("Detail", "Home", new { id = obj.ProjectId });
        }

        //[HttpPost]
        //public IActionResult UpdateStatus(int id, string status)
        //{
        //    _objectiveService.UpdateStatus(id, status);
        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult Kanban(int projectId)
        {
            var project = _projectService.GetById(projectId);
            if (project == null)
            {
                return NotFound();
            }
            var objectives = _objectiveService.GetByProject(project);

            ViewBag.ProjectId = projectId;
            ViewBag.ProjectName = project.Name;
            ViewBag.Project = project; // Truyền cả project object

            return View(objectives);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, [FromBody] ObjectiveUpdateStatusModel model)
        {
            if (model?.Status == null)
            {
                return BadRequest();
            }
            _objectiveService.UpdateStatus(id, model.Status);
            return Ok();
        }
    }
}
