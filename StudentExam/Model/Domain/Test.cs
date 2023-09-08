
using System.ComponentModel.DataAnnotations.Schema;


namespace StudentExam.Model.Domain
{
    public class Test
    {
        public int TestId { get; set; }
        public String TestTitle { get; set; }
        public String TestDesc { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PassScore { get; set; }
        public String Instructions { get; set; }
        public int TotalMarks { get; set; }

        public QuestionSet? QuestionSet { get; set; }
        public int CourseID { get; set; }
        public string Status { get; set; }

        

        
       







    }
}
