using StudentManagementSystem.Models.Domain;

namespace StudentManagementSystem.Models.ViewModels
{
    public class AttendanceViewModel
    {
       

        public Guid id { get; set; }
        public int StudentId { get; set; }
        public int courseId { get; set; }
        public DateTime Date { get; set; }
        public String courseName { get; set; }
        public String Status { get; set; }

        // Navigation property for the student
        public Student Student { get; set; }

        public List<AttendanceViewModel> AttendanceViewModels;

    }

    
}
