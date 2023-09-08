 
namespace StudentExam.Model.Domain
{
    public class TestEnrolledDetails
    {
        public int Id { get; set; }
        public int TestID { get; set; }
        public int StudentId { get; set; }
        public int? ObtainedMarks { get; set; }

        public Test Test { get; set; }
        
    }
}
