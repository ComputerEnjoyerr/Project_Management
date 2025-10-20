using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class Comment : Controller
    {
        public int CommentID { get; set; }
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
