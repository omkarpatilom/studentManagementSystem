using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.Domain
{
    public class Student
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RollNo { get; set; }

        public string UserName { get; set; }
        public String FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Gender { get; set; }
        public long PhoneNo { get; set; }
        public String Address { get; set; }


        public ICollection<EnrollDetails> Enroll { get; set; }
    }
}
