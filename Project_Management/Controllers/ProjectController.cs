using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
