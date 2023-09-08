using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExam.Controllers;
using StudentExam.Data;
using StudentExam.Model.Domain;
using System.Collections.Generic;

namespace StudentExam.Service
{
    public class QuestionSetService : IQuestionSetService
    {
        private readonly TestDbContext _context;

        public QuestionSetService(TestDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddQuestionToSet(QuestionSet set)
        {
            if (set != null && set.QuestionID != null)
            {
                bool questionExists = await _context.Questions.AnyAsync(q => q.QuestionId == set.QuestionID);
                bool questionExistsInTest = await _context.QuestionSets.AnyAsync(s => s.QuestionID == set.QuestionID && s.TestId == set.TestId);
                if (!questionExistsInTest && questionExists)
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

            if (set != null)
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

        public async Task<ICollection<QuestionSet>> GetAllQuestionsetByTestId(int TestId)
        {
            if (TestId == null)
            {
                return null;
            }
            else
            {
                List<QuestionSet> questionSets = await _context.QuestionSets
                    .Where(questionSet => questionSet.TestId == TestId)
                    .ToListAsync();

                return questionSets;
            }
        }

    }
}
