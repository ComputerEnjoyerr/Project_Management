using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project_Management.Services
{
    public interface IChatRoomService
    {
        Task<List<ChatRoom>> GetChatRoomsAsync(int projectId);
        Task<ChatRoom?> GetByIdAsync(int chatRoomId);
        Task CreateChatRoomAsync(ChatRoom room);

        Task<List<Message>> GetMessagesAsync(int chatRoomId);
        Task AddMessageAsync(Message msg);
    }


    public class ChatRoomService : IChatRoomService
    {
        private readonly ProjectManagementDbContext _context;

        public ChatRoomService(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatRoom>> GetChatRoomsAsync(int projectId)
        {
            return await _context.ChatRooms
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<ChatRoom?> GetByIdAsync(int chatRoomId)
        {
            return await _context.ChatRooms
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.ChatRoomId == chatRoomId);
        }

        public async Task CreateChatRoomAsync(ChatRoom room)
        {
            room.CreatedAt = DateTime.Now;
            _context.ChatRooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessagesAsync(int chatRoomId)
        {
            return await _context.Messages
                .Where(x => x.ChatRoomId == chatRoomId)
                .OrderBy(x => x.SentAt)
                .ToListAsync();
        }

        public async Task AddMessageAsync(Message msg)
        {
            msg.SentAt = DateTime.Now;
            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
        }
    }
    // Chức 
    public class ChatHub : Hub
    {
        public async Task SendMessage(int chatRoomId, string user, string content)
        {
            await Clients.Group($"chatroom_{chatRoomId}")
                         .SendAsync("ReceiveMessage", user, content, DateTime.Now.ToString("HH:mm"));
        }

        public override async Task OnConnectedAsync()
        {
            var http = Context.GetHttpContext();
            var chatRoomId = http.Request.Query["chatRoomId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, $"chatroom_{chatRoomId}");

            await base.OnConnectedAsync();
        }
    }


}
