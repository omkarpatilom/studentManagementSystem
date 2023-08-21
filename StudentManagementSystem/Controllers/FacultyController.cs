using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models.Domain;
using StudentManagementSystem.Models.ViewModels;
using System.Data;

namespace StudentManagementSystem.Controllers
{
    public class FacultyController : Controller
    {
        private StudentDbContext StudentDbContext;

        public FacultyController(StudentDbContext studentDbContext)
        {
            StudentDbContext = studentDbContext;
        }



        [HttpGet]
        public IActionResult LoginAuth(string userName, string password)
        {



            // Check if a student with the specified credentials exists in the database
            var Faculty = StudentDbContext.facultyDetails.FirstOrDefault(s => s.Email == userName && s.Password == password);

            if (Faculty != null)
            {
                // Redirect to the HomePage action with the userName and phoneNo values appended to the URL
                return RedirectToAction("HomePage", "Faculty", new { FacultyId = Faculty.FacultyId });
            }
            else
            {
                // Add an error message to the model state if the student is not found
                ModelState.AddModelError("", "Invalid username or phone number");

                // Return the Login view with the updated model state
                return View("Login", "Admin");
            }
        }



        [HttpGet]
        public IActionResult HomePage(int FacultyId)
        {
            List<EnrollDetailsViewModel> enrollDetailsViewModelList = new List<EnrollDetailsViewModel>();

            var Faculty = StudentDbContext.facultyDetails.Find(FacultyId);

            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=studentdb;Uid=root;Pwd=root;"))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetEnrollDetailsByFacultyID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@facultyID", FacultyId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EnrollDetails enrollDetails = new EnrollDetails
                            {
                                EnrollId = Convert.ToInt32(reader["EnrollId"]),
                                EnrollDate = Convert.ToDateTime(reader["EnrollDate"]),
                                // Map other enroll details properties
                            };

                            // Map course details
                            Course course = new Course
                            {
                                CourseId = Convert.ToInt32(reader["CourseId"]),
                                CourseName = reader["CourseName"].ToString(),
                                Fees = (int)reader["Fees"],
                                Description = reader["Description"].ToString(),
                                // Map other course properties
                            };
                            enrollDetails.course = course;

                            // Map faculty details
                            FacultyDetails faculty = new FacultyDetails
                            {
                                //FacultyId = Convert.ToInt32(reader["FacultyId"]),
                                FacultyId = Faculty.FacultyId,
                                FacultyName = Faculty.FacultyName,
                                Address = Faculty.Address,
                                Email = Faculty.Email,
                                designation = Faculty.designation,
                                phoneNo = Faculty.phoneNo


                                // Map other faculty properties
                            };
                            //enrollDetails.FacultyDetailsFacultyId = faculty.FacultyId;
                            enrollDetails.course.FacultyDetails = faculty;



                            // Map student details
                            Student student = new Student
                            {
                                RollNo = Convert.ToInt32(reader["RollNo"]),
                                UserName = reader["UserName"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Gender = reader["Gender"].ToString(),
                                PhoneNo = (long)reader["StudentPhoneNo"],
                                Address = reader["StudentAddress"].ToString(),
                                // Map other student properties
                            };
                            enrollDetails.student = student;

                            EnrollDetailsViewModel enrollDetailsViewModel = new EnrollDetailsViewModel
                            {
                                EnrollId = enrollDetails.EnrollId,
                                StudentId = enrollDetails.StudentId,
                                CourseId = enrollDetails.CourseId,
                                EnrollDate = enrollDetails.EnrollDate,
                                student = enrollDetails.student,
                                course = enrollDetails.course,
                                FacultyDetailsFacultyId = enrollDetails.FacultyDetailsFacultyId,
                                EnrollDetails = enrollDetails


                            };

                            enrollDetailsViewModelList.Add(enrollDetailsViewModel);
                        }
                    }
                }
            }

            return View(enrollDetailsViewModelList);
        }


        [HttpGet]
        public IActionResult FacultyRegister()
        {
            return View();
        }
        public IActionResult FacultyRegister(FacultyViewModel model)
        {


            var NewFaculty = new FacultyDetails()
            {
                FacultyName = model.FacultyName,
                Email = model.Email,
                phoneNo = model.phoneNo,
                Address = model.Address,
                Password = model.Password,
                designation = model.designation

            };

            StudentDbContext.facultyDetails.Add(NewFaculty);
            StudentDbContext.SaveChanges();

            return RedirectToAction("ShowLogin", "Admin");
        }

        public IActionResult Attendance(int courseId, string courseName)
        {
            List<EnrollDetails> enrollments = StudentDbContext.EnrollDetails
                .Where(enrollDetails => enrollDetails.CourseId == courseId)
                .ToList();

            List<AttendanceViewModel> attendanceViewModels = enrollments
                .Select(enrollment =>
                {
                    Student student = StudentDbContext.Students.FirstOrDefault(s => s.RollNo == enrollment.StudentId);
                    return new AttendanceViewModel
                    {
                        StudentId = enrollment.StudentId,
                        courseId = enrollment.CourseId,
                        courseName = courseName,
                        Student = student
                    };
                })
                .ToList();

            return View(attendanceViewModels);
        }


        [HttpPost]
        public ActionResult MarkAttendance(List<AttendanceViewModel> attendanceData)
        {
            try
            {
                foreach (var attendance in attendanceData)
                {
                    var findAttendance = StudentDbContext.attendance.FirstOrDefault(a => a.courseId == attendance.courseId && a.StudentId == attendance.StudentId);
                    if (findAttendance != null)
                    {
                        //update status of exsiting student
                        findAttendance.Status = attendance.Status;
                    }
                    else
                    {
                        // Create a new attendance record
                        var newAttendance = new Attendance
                        {
                            id = Guid.NewGuid(),
                            StudentId = attendance.StudentId,
                            Date = DateTime.Today,
                            Status = attendance.Status,
                            courseId = attendance.courseId
                        };
                        // Add the new attendance record to the database
                        StudentDbContext.attendance.Add(newAttendance);
                    }




                }

                // Save the changes to the database
                StudentDbContext.SaveChanges();

                // Get the courseId from the first attendanceData item
                var courseId = attendanceData.FirstOrDefault()?.courseId;


                // Get the FacultyId from the Courses model using the courseId
                var facultyId = StudentDbContext.Courses.Where(c => c.CourseId == courseId).Select(c => c.FacultyDetailsId).FirstOrDefault();

                return RedirectToAction("HomePage", "Faculty", new { FacultyId = facultyId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error marking attendance. " + ex.Message });
            }
        }

        public IActionResult ShowAttendace(int courseId)
        {
            List<Attendance> attendances = StudentDbContext.attendance.Where(a => a.courseId == courseId).ToList();
            List<EnrollDetails> enrollments = StudentDbContext.EnrollDetails.Where(enrollDetails => enrollDetails.CourseId == courseId).ToList();

            List<AttendanceViewModel> attendanceViewModels = enrollments.Select(enrollment =>
            {
                Student student = StudentDbContext.Students.FirstOrDefault(s => s.RollNo == enrollment.StudentId);
                Attendance attendance = attendances.FirstOrDefault(a => a.StudentId == enrollment.StudentId);

                /*string attendanceStatus = (attendance != null && attendance.Status.Where(a => a. == courseId)) ? "Present" : "Absent";*/

                return new AttendanceViewModel
                {
                    StudentId = enrollment.StudentId,
                    courseId = enrollment.CourseId,
                    Student = student,
                    Status = attendance.Status
                };
            }).ToList();

            return View(attendanceViewModels);
        }



    }
}


