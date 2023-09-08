using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentExam.Model.Domain;
using StudentExam.Service;

namespace StudentExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionSetController : ControllerBase
    {
        private readonly IQuestionSetService _questionSetService;

        public QuestionSetController(IQuestionSetService questionSetService)
        {
            _questionSetService = questionSetService;
        }

        [HttpPost("AddQuestionToSet")]
        public async Task<bool> AddQuestionToSet([FromBody] QuestionSet Set)
        {
            return await _questionSetService.AddQuestionToSet(Set);
        }

        [HttpDelete("DeleteQuestionFromSet")]
        public async Task<bool> DeleteQuestionFromSet(int SetId)
        {
            return await _questionSetService.DeleteQuestionFromSet(SetId);
        }

        [HttpGet("GetAllQuestionsetByTestId/{TestId}")]
        public async Task<IActionResult> GetAllQuestionsetByTestId(int TestId)
        {
            var QuestionSet = await _questionSetService.GetAllQuestionsetByTestId(TestId);

            if (QuestionSet == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(QuestionSet);
            }
        }

    }
}
