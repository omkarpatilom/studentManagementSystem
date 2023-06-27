using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models.Domain;
using StudentManagementSystem.Models.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private StudentDbContext StudentDbContext;

        public CourseController(StudentDbContext StudentDbContext)
        {
            this.StudentDbContext = StudentDbContext;
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            List<FacultyDetails> faculties = StudentDbContext.facultyDetails.ToList();

            // Map the faculty list to SelectListItem for the dropdown
            var facultyOptions = faculties.Select(f => new SelectListItem
            {
                Value = f.FacultyId.ToString(),
                Text = f.FacultyName
            });

            // Create the CourseViewModel and assign the faculty options
            CourseViewModel addCourseViewModel = new CourseViewModel
            {
                FacultyOptions = facultyOptions
            };

            return View(addCourseViewModel);
        }




        [HttpPost]
        public IActionResult AddCourse(CourseViewModel addCourse)
        {
            
                // Find the faculty record corresponding to the selected FacultyDetailsId
                FacultyDetails faculty = StudentDbContext.facultyDetails.FirstOrDefault(f => f.FacultyId == addCourse.FacultyDetailsId);

                if (faculty != null)
                {
                    // Create a new Course object and set its properties
                    var course = new Course()
                    {
                        CourseName = addCourse.CourseName,
                        Fees = addCourse.Fees,
                        Description = addCourse.Description,
                        FacultyDetailsId = addCourse.FacultyDetailsId,
                    };

                    // Add the new Course object to the context and save changes
                    StudentDbContext.Courses.Add(course);
                    StudentDbContext.SaveChanges();

                    return RedirectToAction("GetAllCourses");
                }
            

            // If there is an error or invalid data, reload the view with the same form data
            //addCourse.FacultyDetails = StudentDbContext.facultyDetails.ToList();
            return View(addCourse);
        }



        public IActionResult GetAllCourses()
        {
            var courses = StudentDbContext.Courses
                .Include(c => c.FacultyDetails)
                .ToList();

            var courseViewModels = courses.Select(c => new CourseViewModel
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                FacultyDetailsId = c.FacultyDetailsId,
                FacultyDetails = c.FacultyDetails,
                Fees = c.Fees,
                Description = c.Description,
                Enroll = c.Enroll
            }).ToList();

            return View(courseViewModels);
        }




        public ActionResult ShowStudentAndCourseData()
        {
            List<Student> students = StudentDbContext.Students.Include(s => s.Enroll).ToList();

            CombinedViewModel viewModel = new CombinedViewModel
            {
                Students = students.Select(s => new StudentViewModel
                {
                    RollNo = s.RollNo,
                    UserName = s.UserName,
                    FullName = s.FullName,
                    DateOfBirth = s.DateOfBirth,
                    Gender = s.Gender,
                    PhoneNo = s.PhoneNo,
                    Address = s.Address,
                    
                }).ToList()
            };

            return View(viewModel);
        }








    }
}
