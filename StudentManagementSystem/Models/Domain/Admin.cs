using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Domain
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public String Password { get; set; }
        public String designation { get; set; }
    }
}
