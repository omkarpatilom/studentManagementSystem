using StudentManagementSystem.Models.Domain;

namespace StudentManagementSystem.Models.ViewModels
{
    public class LoginViewModel
    {
        public Student Student { get; set; }
        public List<Course> EnrolledCourses { get; set; }
        public List<FacultyDetails> FacultyMembers { get; set; }
        public EnrollDetails Enrollments { get; set; }

        public Admin admin { get; set; }
    }
}
