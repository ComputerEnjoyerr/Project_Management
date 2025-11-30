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

        Task<IEnumerable<ChatRoom>> GetByProjectIdAsync(int projectId);
        Task<ChatRoom> CreateAsync(ChatRoom room);
        Task<bool> UpdateNameAsync(int id, string newName);
        Task<bool> DeleteAsync(int id);
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

        public async Task<IEnumerable<ChatRoom>> GetByProjectIdAsync(int projectId)
        {
            return await _context.ChatRooms
                .Where(x => x.ProjectId == projectId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ChatRoom> CreateAsync(ChatRoom room)
        {
            _context.ChatRooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<bool> UpdateNameAsync(int id, string newName)
        {
            var room = await _context.ChatRooms.FindAsync(id);
            if (room == null) return false;

            room.Name = newName;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var room = await _context.ChatRooms.FindAsync(id);
            if (room == null) return false;

            _context.ChatRooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
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
