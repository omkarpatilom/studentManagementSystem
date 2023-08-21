using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models.Domain;
using StudentManagementSystem.Models.ViewModels;
using System.Runtime.CompilerServices;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private StudentDbContext StudentDbContext;

        public StudentController(StudentDbContext studentDbContext)
        {
            this.StudentDbContext = studentDbContext;
        }


        [HttpGet]
        public IActionResult StudentForm()
        {
            List<Course> AllCourses = StudentDbContext.Courses.ToList();
            /*var viewModel = new StudentViewModel
            {
                CourseList = StudentDbContext.Courses
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.CourseName
                    })
                    .ToList()
            };*/

            return View();
        }

        [HttpPost]
        public IActionResult StudentForm(StudentViewModel addStudentRequest)
        {
            

            // Create a new Student object with the form data
            var student = new Student()
            {
                RollNo = addStudentRequest.RollNo,
                FullName = addStudentRequest.FullName,
                UserName = addStudentRequest.UserName,
                DateOfBirth = addStudentRequest.DateOfBirth,
                Gender = addStudentRequest.Gender,
                PhoneNo = addStudentRequest.PhoneNo,
                Address = addStudentRequest.Address
            };

            // Create a new EnrollDetails object with the selected course ID and the student's roll number
            /*var enrollDetails = new EnrollDetails()
            {
                StudentId = addStudentRequest.RollNo,
                CourseId = addStudentRequest.CourseId,
                EnrollDate = DateTime.Today
            };*/

            // Add the Student and EnrollDetails objects to the database using the StudentDbContext
            StudentDbContext.Students.Add(student);
             StudentDbContext.SaveChanges();
            
         

            return RedirectToAction("ShowLogin", "Admin");
        }



        [HttpGet]
        public IActionResult GetAllStudents ()
        {
            var Students = StudentDbContext.Students.ToList();

            return View(Students);
        }

        [HttpGet]
        public IActionResult UpdateStudent(int rollno)
        {
            var student = StudentDbContext.Students.Find(rollno);

            if (student != null)
            {

                var viewModel = new StudentViewModel()
                {
                    RollNo = student.RollNo,
                    FullName = student.FullName,
                    UserName = student.UserName,
                    DateOfBirth = student.DateOfBirth,
                    Gender = student.Gender,
                    PhoneNo = student.PhoneNo,
                    Address = student.Address
                };
                return View("UpdateStudent", viewModel);

            }
            return RedirectToAction("GetAllStudents");
        }
        [HttpPost]
        public IActionResult UpdateStudent(StudentViewModel model)
        {
            var student = StudentDbContext.Students.Find(model.RollNo);

            if (student != null)
            {
                student.RollNo = model.RollNo;
                student.FullName=model.FullName;
                student.UserName = model.UserName;
                student.Gender = model.Gender;
                student.DateOfBirth=model.DateOfBirth;
                student.PhoneNo=model.PhoneNo;
                student.Address=model.Address;

                StudentDbContext.SaveChanges();
                return RedirectToAction("GetAllStudents");
            }


            return RedirectToAction("GetAllStudents");
        }


        [HttpPost]
        public IActionResult DeleteStudent(StudentViewModel model)
        {
            var student =  StudentDbContext.Students.Find(model.RollNo);

            if (student != null)
            {
                StudentDbContext.Students.Remove(student);
                StudentDbContext.SaveChanges();

                return RedirectToAction("GetAllStudents");
            }

            return RedirectToAction("GetAllStudents");
        }

        [HttpGet]
        public IActionResult DeleteFromList(int rollno)
        {
            var student = StudentDbContext.Students.Find(rollno);

            if (student != null)
            {
                StudentDbContext.Remove(student);
                StudentDbContext.SaveChanges();
            }
            return RedirectToAction("GetAllStudents");
        }



        [HttpGet]
        public IActionResult LoginAuth(string userName, string password)
        {
            // Convert the phoneNo string to a long data type using long.TryParse method
           long phone = long.Parse(password);

            // Check if a student with the specified credentials exists in the database
            var student = StudentDbContext.Students.FirstOrDefault(s => s.UserName == userName && s.PhoneNo == phone);

            if (student != null)
            {
                // Redirect to the HomePage action with the userName and phoneNo values appended to the URL
                return RedirectToAction("HomePage", new { rollNo=student.RollNo });
            }
            else
            {
                // Add an error message to the model state if the student is not found
                ModelState.AddModelError("", "Invalid username or phone number");

                // Return the Login view with the updated model state
                return View("Login","Admin");
            }
        }
       


        [HttpGet]
        public IActionResult HomePage(int rollNo)
        {
            var student = StudentDbContext.Students.FirstOrDefault(s => s.RollNo == rollNo);

            if (student == null)
            {
                return NotFound();
            }

            var enrollments = StudentDbContext.EnrollDetails
                .Where(e => e.StudentId == student.RollNo)
                .ToList();

            var enrolledCourses = new List<Course>();

            foreach (var enrollment in enrollments)
            {
                var course = StudentDbContext.Courses.Find(enrollment.CourseId);
                var faculty = StudentDbContext.facultyDetails.FirstOrDefault(f => f.FacultyId == course.FacultyDetailsId);

                if (course != null)
                {
                    course.FacultyDetails = faculty;
                    enrolledCourses.Add(course);
                }
            }

            
            // Get the list of available courses for the dropdown
            var enrolledCourseIds = enrollments.Select(e => e.CourseId).ToList();

            var availableCourses = StudentDbContext.Courses
                .Where(c => !enrolledCourseIds.Contains(c.CourseId))
                .ToList();

            ViewBag.Student = student;
            ViewBag.EnrolledCourses = enrolledCourses;
            ViewBag.Enrollments = enrollments;
            ViewBag.AvailableCourses = availableCourses;

            return View();
        }


        public IActionResult PayNow(int enrollmentId)
        {
            // Assuming you have a database context
            var enrollment = StudentDbContext.EnrollDetails.FirstOrDefault(e => e.EnrollId == enrollmentId);
            if (enrollment != null)
            {
                enrollment.FeesStatus = "Paid";
                StudentDbContext.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }



        [HttpPost]
        public IActionResult CreateCourse(int rollNo, int courseId)
        {

            // Retrieve the current student from the database
            var student = StudentDbContext.Students.Find(rollNo);
            var Course = StudentDbContext.Courses.Find(courseId);
            

            if (student == null)
            {
                // If the student is not found, handle the situation accordingly
                // For example, you can redirect to an error page or display a message
                return RedirectToAction("Error");
            }

            // Clone the student object to create a new instance with the same properties
            EnrollDetails Enroll = new EnrollDetails
            {
                StudentId=rollNo,
                CourseId=courseId,
                EnrollDate=DateTime.Today,
                FacultyDetailsFacultyId = Course.FacultyDetailsId,
                FeesStatus="Unpaid"
                
               
                
                
                
            };

            // Add the new student to the database and save the changes

            StudentDbContext.EnrollDetails.Add(Enroll);
            StudentDbContext.SaveChanges();

            return RedirectToAction("HomePage", new { rollNo = student.RollNo });
        }

       


    }
}
