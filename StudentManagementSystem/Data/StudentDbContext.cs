using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models.Domain;

namespace StudentManagementSystem.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<FacultyDetails> facultyDetails { get; set; }
        public DbSet<EnrollDetails> EnrollDetails { get; set; }
        public DbSet<Attendance> attendance { get; set; }

    }
}
