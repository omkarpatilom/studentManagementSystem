// QuestionService.cs (Modified)

using Microsoft.AspNetCore.Mvc;
using StudentExam.Data;
using StudentExam.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentExam.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly TestDbContext _context;

        public QuestionService(TestDbContext context)
        {
            _context = context;
        }

        public bool CreateQuestion(QuestionDetails question)
        {
            if (question != null)
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public string DeleteQuestionById(int id)
        {
            var question = GetQuestionById(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
                return "Id Deleted Successfully";
            }

            return "Id Not Found";
        }

        public ICollection<QuestionDetails> GetAllQuestions()
        {
            return _context.Questions.ToList();
        }

        public QuestionDetails GetQuestionById(int id)
        {
            return _context.Questions.FirstOrDefault(q => q.QuestionId == id);
        }

        public QuestionDetails GetQuestionByName(string QName)
        {
            return _context.Questions.FirstOrDefault(q => q.QuestionDescription == QName);
        }

        public ICollection<QuestionDetails> GetQuestionsByCourse(int courseId)
        {
            return _context.Questions.Where(q=>q.CourseID == courseId).ToList();

        }

        public bool IsQuestionExisted(int id)
        {
            return _context.Questions.Any(q => q.QuestionId == id);
        }

        public QuestionDetails UpdateQuestion(int id, QuestionDetails question)
        {
            var existingQuestion = GetQuestionById(id);
            if (existingQuestion != null)
            {
                existingQuestion.QuestionDescription = question.QuestionDescription;
                existingQuestion.Option1 = question.Option1;
                existingQuestion.Option2 = question.Option2;
                existingQuestion.Option3 = question.Option3;
                existingQuestion.Option4 = question.Option4;
                existingQuestion.CorrectAns = question.CorrectAns;

                _context.Update(existingQuestion);
                _context.SaveChanges();

                return existingQuestion;
            }

            return null;
        }
    }
}
