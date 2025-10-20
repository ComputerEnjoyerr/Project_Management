using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class TimeEntry : Controller
    {
        public int TimeEntryID { get; set; }
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
