using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExam.Data;
using StudentExam.Model.Domain;

namespace StudentExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestDbContext _context;

        public TestController(TestDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Test test)
        {
            if (ModelState.IsValid)
            {
                _context.Tests.Add(test);
                await _context.SaveChangesAsync();
                return Ok(test);
            }
            else
                return BadRequest();
        }

        [HttpPost("CreateQuestionSetapi")]
        public async Task<IActionResult> CreateQuestionSet(QuestionSet set)
        {
            if (ModelState.IsValid)
            {
                _context.QuestionSets.Add(set);
                await _context.SaveChangesAsync();
                return Ok(set);
            }

            return BadRequest();

        }

        [HttpPost("QuestionSetsapi")]
        public async Task<IActionResult> QuestionSets([FromBody] int courseId)
        {
            List<Test> testDetails = _context.Tests
                .Where(t => t.CourseID == courseId)
                .ToList();



            if (testDetails.Count > 0)
            {
                return Ok(testDetails);
            }
            else
            {
                return BadRequest(testDetails);
            }

        }

        [HttpGet("GetTestDetails")]
        public async Task<IActionResult> GetTestDetails(int TestId)
        {
            if (TestId != null || TestId > 0)
            {
                Test Testinfo = await _context.Tests.FirstOrDefaultAsync(T => T.TestId == TestId);

                if (Testinfo != null)
                {
                    return Ok(Testinfo);
                }
                else
                    return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPatch("UpdateTest/{TestId}")]
        public async Task<IActionResult> UpdateTest(int TestId, [FromBody] Test test)
        {
            if (TestId != null && test != null)
            {
                var ExsitedTest = await _context.Tests.FirstOrDefaultAsync(t => t.TestId == TestId);

                if (ExsitedTest != null)
                {
                    ExsitedTest.TestTitle = test.TestTitle ?? ExsitedTest.TestTitle;
                    ExsitedTest.TestDesc = test.TestDesc ?? ExsitedTest.TestDesc;
                    ExsitedTest.StartTime = test.StartTime;
                    ExsitedTest.EndTime = test.EndTime;
                    ExsitedTest.CourseID = test.CourseID;
                    ExsitedTest.Instructions = test.Instructions;
                    ExsitedTest.PassScore = test.PassScore;
                    ExsitedTest.TotalMarks = test.TotalMarks;
                    ExsitedTest.Status = test.Status;


                    _context.Tests.Update(ExsitedTest);
                    _context.SaveChangesAsync();
                    return Ok(ExsitedTest);


                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

    }
}
