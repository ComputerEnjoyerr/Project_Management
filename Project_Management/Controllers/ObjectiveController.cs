using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using System.Security.Claims;

namespace Project_Management.Controllers
{

    public class ObjectiveController : Controller
    {
        private readonly ProjectManagementDbContext _context;

        public ObjectiveController(ProjectManagementDbContext context)
        {
            _context = context;
        }
        //công việc của tôi
        public IActionResult Index()
        {
            var userName = User.FindFirstValue(ClaimTypes.Email);
            if (userName == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            return View();
        }
        // 👉 Thêm công việc
        public IActionResult Create(int projectId)
        {
            var newObjective = new Objective
            {
                ProjectId = projectId,
                Status = "Todo"
            };
            return View(newObjective);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Objective objective)
        {
            var userName = User.FindFirstValue(ClaimTypes.Email);
            if (userName == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            // Kiểm tra model hợp lệ
            if (!ModelState.IsValid)
            {
                return View(objective);
            }

            // ✅ Vì bạn KHÔNG có field CreatedAt trong model, nên ta tạo biến riêng
            var createdAt = DateTime.Now;

            // Gán email người tạo hoặc người được giao (nếu bạn muốn)
            if (string.IsNullOrEmpty(objective.AssignedToEmail))
            {
                objective.AssignedToEmail = userName;
            }

            // Lưu vào database
            _context.Add(objective);
            await _context.SaveChangesAsync();

            // ✅ Nếu DB có cột CreatedAt nhưng model không có, thì cập nhật thủ công
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "UPDATE Objectives SET CreatedAt = {0} WHERE ObjectiveId = {1}",
                    createdAt, objective.ObjectiveId);
            }
            catch
            {
                // Nếu DB không có cột CreatedAt, ta chỉ hiển thị tạm thôi
            }

            // ✅ Dùng TempData để hiển thị thông tin thời gian tạo
            TempData["CreatedAt"] = createdAt.ToString("HH:mm dd/MM/yyyy");
            TempData["Message"] = "Tạo công việc mới thành công!";

            // Quay về chi tiết project
            return RedirectToAction("Details", "Project", new { id = objective.ProjectId });
        }

        // ✏️ Sửa công việc
        public async Task<IActionResult> Edit(int id)
        {
            var obj = await _context.Objectives.FindAsync(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Objective obj)
        {
            if (id != obj.ObjectiveId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Project", new { id = obj.ProjectId });
            }
            return View(obj);
        }

        // ❌ Xóa công việc
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _context.Objectives.FindAsync(id);
            if (obj == null) return NotFound();

            int projectId = obj.ProjectId;
            _context.Objectives.Remove(obj);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Project", new { id = projectId });
        }
    }
}
