using Microsoft.EntityFrameworkCore;
using StudentExam.Model.Domain;

namespace StudentExam.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<QuestionDetails> Questions { get; set; }
        public DbSet<QuestionSet> QuestionSets { get; set; }
        public DbSet<Test> Tests { get; set; }


        

    }
    
}
