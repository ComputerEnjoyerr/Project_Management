using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? UserName{ get; set; }
        public string? Password { get; set; } // In a real application, passwords should be hashed and salted
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public  Role Role { get; set; } // e.g., Admin, Manager, Employee
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
