using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.Domain
{
    public class EnrollDetails
    {
       

        [Key]
        public int EnrollId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime EnrollDate { get; set; }
        public String FeesStatus { get; set; }
        public Student student { get; set; }
        public Course course { get; set; }
        public int FacultyDetailsFacultyId { get; set; }
    }
}
