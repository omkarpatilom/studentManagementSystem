using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentExam.Data;
using StudentExam.Model.Domain;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models.Domain;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace StudentManagementSystem.Controllers
{
    public class StudentTestController : Controller
    {
        private readonly HttpClient _httpClient;
       
        private readonly StudentDbContext _studentDbContext;

        public StudentTestController(HttpClient httpClient, StudentDbContext studentDbContext)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7217/");
            _studentDbContext = studentDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int TestId)
        {
            var response = await _httpClient.GetAsync("api/QuestionDetails/GetAllQuestions");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);

                List<QuestionDetails> questions = JsonConvert.DeserializeObject<List<QuestionDetails>>(data);
                return View(questions);

            }
            else
            {
                return BadRequest();
            }
           
        }
        [HttpGet]
        public async Task<IActionResult> CreateTest(int courseId)
        {
            var course = _studentDbContext.Courses.FirstOrDefault(c=>c.CourseId==courseId);
            ViewBag.course = course;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(Test test)
        {
            if (test != null)
            {
                var endpoint = "api/Test/Create";

                // Send POST request to destination API
                var destinationResponse = await _httpClient.PostAsJsonAsync(endpoint, test);

                if (destinationResponse.IsSuccessStatusCode)
                {
                    // Handle successful data transfer
                    // For example, you can redirect to a success page
                    return RedirectToAction("QuestionSets", "StudentTest", new { courseId = test.CourseID});
                }
                else
                {
                    // Handle unsuccessful data transfer
                    // For example, you can show an error message on the view
                    ModelState.AddModelError("", "Failed to transfer data to destination API.");
                    return View();
                }
            }

            return View();
        }
        
        public async Task<IActionResult> QuestionSets(int courseId)
        {
            
            if (ModelState.IsValid)
            {
                var endpoint = "api/Test/QuestionSetsapi";
                Object payload = new { courseId };
                var payloadJson = JsonConvert.SerializeObject(payload); // Add Newtonsoft.Json package if needed
                Console.WriteLine($"Sending payload: {payloadJson}");

                var destinationResponse = await _httpClient.PostAsJsonAsync(endpoint, (int) courseId);
                ViewBag.CourseID = courseId;

                if (destinationResponse.IsSuccessStatusCode)
                {
                    var responseContent = await destinationResponse.Content.ReadAsStringAsync();

                    // Deserialize the response content to a list of Test objects
                    var testDetails = JsonConvert.DeserializeObject<List<Test>>(responseContent); 

                    //return RedirectToAction("CreateQuestionSet", "Test",new { testDetails });
                    return View(testDetails);
                }
                else
                {
                    // Handle unsuccessful data transfer
                    // For example, you can show an error message on the view
                    ModelState.AddModelError("", "Failed to transfer data to destination API.");
                    return View();
                }
                

            }
            return View();
        }

        public async Task<IActionResult> addQuestionToSet(int TestId)
        {
            var endpoint = $"api/Test/GetTestDetails?TestId={(int)TestId}";

            var destinationResponse = await _httpClient.GetAsync(endpoint);

            if(destinationResponse.IsSuccessStatusCode)
            {
                var responseContent = await destinationResponse.Content.ReadAsStringAsync();

                // Deserialize the response content to a list of Test objects
                var testDetails = JsonConvert.DeserializeObject<Test>(responseContent);

                var CourseID = testDetails.CourseID;

               if(CourseID!=null || CourseID > 0)
                {
                     var path = $"api/QuestionDetails/GetQuestionsByCourse?CourseId={CourseID}";

                    var Response = await _httpClient.GetAsync(path);

                    if(Response.IsSuccessStatusCode)
                    {
                         var responseResult = await Response.Content.ReadAsStringAsync();

                        // Deserialize the response content to a list of Test objects
                        var QuestionList  = JsonConvert.DeserializeObject<List<QuestionDetails>>(responseResult);
                        ViewBag.TestId = TestId;
                        return View(QuestionList);
                    }

                }
                return View();
            }
            else
            {

                // Handle unsuccessful data transfer
                // For example, you can show an error message on the view
                ModelState.AddModelError("", "Failed to transfer data to destination API.");
                return View();
            }
        }

        public async Task<IActionResult> AddQuestion(QuestionSet set)
        {
            // Handle the data, and save it to the database
            // You can access the properties of the 'set' object here

            // Return appropriate response, like a redirection or JSON response
            return View();
        }


    }
}
