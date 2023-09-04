using System.ComponentModel.DataAnnotations;

namespace StudentExam.Model.Domain
{
    public class QuestionDetails
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; }
        public String? Option1 { get; set; }
        public String? Option2 { get; set; }
        public String? Option3 { get; set; }
        public String? Option4 { get; set; }
        public String? CorrectAns { get; set; }
        public int? CourseID { get; set; }

    }
}
