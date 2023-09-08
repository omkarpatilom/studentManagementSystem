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
        public DbSet<TestEnrolledDetails> TestEnrolledDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuestionSet>()
                .HasIndex(qs => qs.TestId)
                .IsUnique(false); // This allows duplicate TestId values
        }



    }
    
}
