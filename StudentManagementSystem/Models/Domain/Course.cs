using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using StudentManagementSystem.Models.Domain;

namespace StudentManagementSystem.Models.Domain
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        [Column("FacultyId")]
        public int FacultyDetailsId { get; set; }
        public int Fees { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        

        [Column("FacultyId")]
        public FacultyDetails FacultyDetails { get; set; }

        public ICollection<EnrollDetails> Enroll { get; set; }

    }
}
