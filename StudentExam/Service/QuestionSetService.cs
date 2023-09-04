using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExam.Controllers;
using StudentExam.Data;
using StudentExam.Model.Domain;

namespace StudentExam.Service
{
    public class QuestionSetService : IQuestionSetService
    {
        private readonly TestDbContext _context;

        public QuestionSetService(TestDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddQuestionToSet( QuestionSet set)
        {
            if(set != null && set.QuestionID!=null)
            {
               bool questionExists = await _context.Questions.AnyAsync(q=>q.QuestionId==set.QuestionID);
                if(questionExists)
                {
                    _context.QuestionSets.Add(set);
                     await _context.SaveChangesAsync();
                    return true;
                }
                
            }
            return false;
           
        }

        public async Task<bool> DeleteQuestionFromSet(int SetId)
        {
            QuestionSet set = await _context.QuestionSets.FirstOrDefaultAsync(S => S.QuestionSetId == SetId);

            if(set != null)
            {
                _context.QuestionSets.Remove(set);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
