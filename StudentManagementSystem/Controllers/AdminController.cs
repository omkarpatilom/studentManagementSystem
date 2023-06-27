using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models.Domain;
using StudentManagementSystem.Models.ViewModels;



namespace StudentManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private StudentDbContext StudentDbContext;

        public AdminController(StudentDbContext studentDbContext)
        {
            this.StudentDbContext = studentDbContext;
        }

        public IActionResult ShowLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            string userName = Request.Form["UserName"];
            string password = Request.Form["Password"];
            string role = Request.Form["SelectedRole"];


            HttpContext.Session.SetString("UserRole", role);

            switch (role)
            {
                case "Student":
                    return RedirectToAction("LoginAuth", "Student", new { username = userName, password = password });

                case "Faculty":
                    return RedirectToAction("LoginAuth", "Faculty", new { username = userName, password = password });

                case "Admin":
                    return RedirectToAction("LoginAuth", "Admin", new { username = userName, password = password });

                default:
                    // Invalid user type
                    ModelState.AddModelError("", "Invalid user type selected.");
                    return View();
            }
        }


        public IActionResult LoginAuth(String Username, String Password)
        {
            // Check if a admin with the specified credentials exists in the database
            var adminData = StudentDbContext.admins.FirstOrDefault(s => s.Email == Username && s.Password == Password);


             if (adminData != null)
             {
                 return RedirectToAction("AdminHomePage", "Admin", new { Username = Username, Password = Password });

             }
             else
             {
                 // Add an error message to the model state if the student is not found
                 ModelState.AddModelError("", "Invalid username or phone number");

                 // Return the Login view with the updated model state
                 return View("ShowLogin", "Admin");
             }
            
        }

        public IActionResult AdminHomePage(String Username, String Password)
        {

            // Retrieve Admin data from the database for the given Email
            var admin = StudentDbContext.admins.FirstOrDefault(s => s.Email.Equals(Username) && s.Password.Equals(Password));

            List<Course> allCourses = StudentDbContext.Courses.ToList();

            List<EnrollDetails> enroll = StudentDbContext.EnrollDetails.ToList();

            if (admin == null)
            {
                RedirectToAction("Login", "Admin");
            }

            ViewBag.Admin = admin;
            ViewBag.AllCourses=allCourses;
            ViewBag.Enroll = enroll;


            return View();
        }

    }
}
