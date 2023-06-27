using StudentManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class FacultyViewModel
    {
        [Key]
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string Email { get; set; }
        public String Password { get; set; }
        public long phoneNo { get; set; }
        public String Address { get; set; }
        public String designation { get; set; }

        public ICollection<EnrollDetails> Enroll { get; set; }
    }
}
