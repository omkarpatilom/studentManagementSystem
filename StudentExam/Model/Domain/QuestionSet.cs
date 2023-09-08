using Microsoft.EntityFrameworkCore;

namespace StudentExam.Model.Domain
{
    public class QuestionSet
    {
        public int QuestionSetId { get; set; }
        public int QuestionID { get; set; }
        public int Marks { get; set; }

        


        // Navigation property to the Test entity (optional)
        public Test? Test { get; set; }

        // Navigation property to the QuestionDetails entity (optional)
        public QuestionDetails? Question { get; set; }

       
        public int? TestId { get; set; } // Optional TestId


    }
}
