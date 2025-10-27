using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Controllers
{
    public class ChatRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
