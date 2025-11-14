using Microsoft.AspNetCore.Mvc;
using Project_Management.Services;

namespace Project_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        // Tạo phòng chat
        [HttpPost("create")]
        public IActionResult CreateChatRoom(int projectId, string name)
        {
            var chatRoom = _chatRoomService.CreateChatRoom(projectId, name);
            return Ok(chatRoom);
        }

        // Lấy các tin nhắn của phòng chat
        [HttpGet("messages/{chatRoomId}")]
        public IActionResult GetMessages(int chatRoomId)
        {
            var messages = _chatRoomService.GetMessages(chatRoomId);
            return Ok(messages);
        }

        // Gửi tin nhắn
        [HttpPost("send")]
        public IActionResult SendMessage(int chatRoomId, string senderEmail, string content)
        {
            _chatRoomService.SendMessage(chatRoomId, senderEmail, content);
            return Ok();
        }
    }
}
