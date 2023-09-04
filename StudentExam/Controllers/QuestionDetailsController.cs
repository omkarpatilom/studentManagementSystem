
using Microsoft.AspNetCore.Mvc;
using StudentExam.Model.Domain;
using StudentExam.Service;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace StudentExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionDetailsController : ControllerBase
    {
        private readonly IQuestionService questionService;

        public QuestionDetailsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpPost("CreateQuestion")]
        public IActionResult CreateQuestion([FromBody] QuestionDetails question)
        {
            if (question == null)
            {
                return BadRequest();
            }

            var result = questionService.CreateQuestion(question);
            return result ? Ok() : StatusCode(500);
        }

        [HttpDelete("DeleteQuestionById/{id}")]
        public IActionResult DeleteQuestionById(int id)
        {
            var result = questionService.DeleteQuestionById(id);
            if (result == "Id Not Found")
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetAllQuestions")]
        public IActionResult GetAllQuestions()
        {
            var questions = questionService.GetAllQuestions();
            return Ok(questions);
        }

        [HttpGet("GetQuestionById/{id}")]
        public IActionResult GetQuestionById(int id)
        {
            var question = questionService.GetQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        [HttpGet("GetQuestionByName/{QName}")]
        public IActionResult GetQuestionByName(string QName)
        {
            var question = questionService.GetQuestionByName(QName);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        [HttpPut("UpdateQuestion/{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody] QuestionDetails question)
        {
            var updatedQuestion = questionService.UpdateQuestion(id, question);
            if (updatedQuestion == null)
            {
                return NotFound();
            }

            return Ok(updatedQuestion);
        }

        [HttpGet("GetQuestionsByCourse")]
        public IActionResult GetQuestionsByCourse(int CourseId)
        {
            var Questions = questionService.GetQuestionsByCourse(CourseId);

            if(Questions == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Questions);
            }
        }

        
    }
}
