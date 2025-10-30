using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project_Management.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            var userName = User.FindFirstValue(ClaimTypes.Email);
            if (userName == null)
            {
                return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
            }
            return View();
        }
    }
}
