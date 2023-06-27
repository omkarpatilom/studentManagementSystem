using StudentManagementSystem.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModels
{
    public class EnrollDetailsViewModel
    { 

        [Key]
        public int EnrollId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime EnrollDate { get; set; }

        public Student student { get; set; }
        public Course course { get; set; }
        public int FacultyDetailsFacultyId { get; set; }
        public EnrollDetails EnrollDetails { get; set; }
        public string FacultyName { get; set; }
       

    }
}
