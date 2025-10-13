using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } 
        public Collection<User> Users { get; set; }
    }
}
