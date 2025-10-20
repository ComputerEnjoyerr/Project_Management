using Microsoft.AspNetCore.Mvc;

namespace Project_Management.Models
{
    public class Stage : Controller
    {
        public int StageID { get; set; }
        public int ProjectID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
