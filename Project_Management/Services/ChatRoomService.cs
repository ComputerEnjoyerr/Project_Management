using Project_Management.Data;
using Project_Management.Models;
using System.Linq;

namespace Project_Management.Services
{
    public interface IChatRoomService
    {
        ChatRoom CreateChatRoom(int projectId, string name);
        List<Message> GetMessages(int chatRoomId);
        void SendMessage(int chatRoomId, string senderEmail, string content);
    }

    public class ChatRoomService : IChatRoomService
    {
        private readonly ProjectManagementDbContext _context;

        public ChatRoomService(ProjectManagementDbContext context)
        {
            _context = context;
        }

        // Tạo phòng chat mới
        public ChatRoom CreateChatRoom(int projectId, string name)
        {
            var chatRoom = new ChatRoom
            {
                ProjectId = projectId,
                Name = name,
                CreatedAt = DateTime.Now
            };
            _context.ChatRooms.Add(chatRoom);
            _context.SaveChanges();
            return chatRoom;
        }

        // Lấy các tin nhắn trong phòng chat
        public List<Message> GetMessages(int chatRoomId)
        {
            return _context.Messages
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .ToList();
        }

        // Gửi tin nhắn
        public void SendMessage(int chatRoomId, string senderEmail, string content)
        {
            var message = new Message
            {
                ChatRoomId = chatRoomId,
                SenderEmail = senderEmail,
                Content = content,
                SentAt = DateTime.Now
            };
            _context.Messages.Add(message);
            _context.SaveChanges();
        }
    }
}
