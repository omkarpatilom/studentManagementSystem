using Microsoft.AspNetCore.Mvc;
using StudentExam.Model.Domain;

namespace StudentExam.Service
{
    public interface IQuestionService
    {
        public bool CreateQuestion(QuestionDetails question);
        public QuestionDetails GetQuestionByName(string QName);
        public QuestionDetails GetQuestionById(int id);
        public ICollection<QuestionDetails> GetAllQuestions();
        public QuestionDetails UpdateQuestion(int id,QuestionDetails question);
        public String DeleteQuestionById(int id);
        public bool IsQuestionExisted(int id);
        public ICollection<QuestionDetails> GetQuestionsByCourse(int courseId);


    }
}
