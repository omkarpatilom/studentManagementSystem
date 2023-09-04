using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentExam.Model.Domain;

namespace StudentExam.Service
{
    public interface IQuestionSetService
    {
          Task<bool> AddQuestionToSet(QuestionSet set);

          Task<bool> DeleteQuestionFromSet(int SetId);
    }
}
