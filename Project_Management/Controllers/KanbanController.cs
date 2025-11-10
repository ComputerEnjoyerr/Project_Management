using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using Project_Management.Services;
using SQLitePCL;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Management.Controllers
{
    public class KanbanController : Controller
    {
        private readonly IKanbanService _kanbanService;
        private readonly IDashboardService _dashboardService;

        public KanbanController(IKanbanService kanbanService, IDashboardService dashboardService)
        {
            _kanbanService = kanbanService;
            _dashboardService = dashboardService;
        }

        public IActionResult Index(int projectId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            var kanbanData = _dashboardService.GetDashboardData(userEmail);
            if (kanbanData == null)
            {
                return RedirectToAction("Index", "Home"); // hoặc trang danh sách project
            }

            return View(kanbanData);
        }

         [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult UpdateTaskStatus(int taskId, string newStatus, int projectId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            if (taskId <= 0 || string.IsNullOrEmpty(newStatus))
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return RedirectToAction("Index", new { projectId });
            }

            var success = _kanbanService.UpdateTaskStatus(taskId, newStatus, userEmail);
            if (success)
            {
                TempData["Success"] = "Cập nhật trạng thái thành công!";
            }
            else
            {
                TempData["Error"] = "Không thể cập nhật trạng thái.";
            }

            return RedirectToAction("Index", new { projectId });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult CreateTask(CreateTaskRequest request, int projectId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            // Gán lại projectId từ route/query (tránh fake)
            request.ProjectId = projectId;

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Vui lòng điền đầy đủ thông tin.";
                return RedirectToAction("Index", new { projectId });
            }

            if (!_kanbanService.CanUserAccessProject(projectId, userEmail))
            {
                TempData["Error"] = "Bạn không có quyền tạo task trong dự án này.";
                return RedirectToAction("Index", new { projectId });
            }

            var taskId = _kanbanService.CreateTask(request, userEmail);
            if (taskId > 0)
            {
                TempData["Success"] = "Tạo task thành công!";
            }
            else
            {
                TempData["Error"] = "Không thể tạo task.";
            }

            return RedirectToAction("Index", new { projectId });
        }

        public IActionResult GetTaskDetails(int taskId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return PartialView("_TaskDetailsPartial", null);
            }

            var objective = _kanbanService.GetTaskDetails(taskId);
            return PartialView("_TaskDetailsPartial", objective ?? new object());
        }
    }
}
