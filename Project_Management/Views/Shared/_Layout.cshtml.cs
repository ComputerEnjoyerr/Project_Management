using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Views.Shared
{
    public class _Layout : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
