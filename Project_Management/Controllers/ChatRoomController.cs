using Microsoft.AspNetCore.Mvc;
using Project_Management.Models;
using Project_Management.Services;
using System.Security.Claims;

public class ChatRoomController : Controller
{
    private readonly IChatRoomService _service;
    private readonly IUserService _userService;
    private readonly IProjectService _projectService;
    public ChatRoomController(IChatRoomService service, IUserService userService, IProjectService projectService)
    {
        _service = service;
        _userService = userService;
        _projectService = projectService;
    }

    // Danh sách chatroom trong 1 project
    public async Task<IActionResult> Index(int projectId)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail == null)
        {
            return Redirect("~/Identity/Account/Login"); // Identity tự động bỏ qua Pages
        }
        var rooms = await _service.GetChatRoomsAsync(projectId);
        var project = _projectService.GetById(projectId);
        if (project != null)
            ViewBag.Project = project;
        return View(rooms);
    }

    // Xem 1 chatroom
    public async Task<IActionResult> Room(int id)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail == null) return Redirect("~/Identity/Account/Login");

        var room = await _service.GetByIdAsync(id);
        if (room == null) return NotFound();
        ViewBag.UserEmail = userEmail;
        // Sắp xếp theo thời gian gửi
        var messages = room.Messages.OrderBy(m => m.SentAt);
        return PartialView("_MessagesPartial", messages);
    }

    // Lưu message
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (userEmail == null)
        {
            return Redirect("~/Identity/Account/Login");
        }
        var msg = new Message
        {
            ChatRoomId = dto.ChatRoomId,
            SenderEmail = userEmail,
            Content = dto.Content,
            SentAt = DateTime.UtcNow
        };

        await _service.AddMessageAsync(msg);

        return Ok();
    }

}
