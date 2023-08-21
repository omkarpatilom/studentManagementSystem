using StudentManagementSystem.Models.Domain;

namespace StudentManagementSystem.Models.ViewModels
{
    public class CombinedViewModel
    {
        public List<EnrollDetailsViewModel> EnrollDetails { get; set; }
        public List<StudentViewModel> Students { get; set; }
        public List<CourseViewModel> Courses { get; set; }
        public List<FacultyDetails> FacultyDetails { get; internal set; }
        /*public List<AdminViewModel> Admins { get; set; }*/
        // Add other properties as needed
    }
}
