using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Models.Domain
{
    public class Attendance
    {
        public Guid id { get; set; }
        public int StudentId { get; set; }
        public int courseId { get; set; }
        public DateTime Date { get; set; }
        public String Status { get; set; }

        // Navigation property for the student
        public Student Student { get; set; }

    }

    
}
