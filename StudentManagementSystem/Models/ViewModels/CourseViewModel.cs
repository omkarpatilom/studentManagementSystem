using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Models.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudentManagementSystem.Models.ViewModels
{
    public class CourseViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        public string CourseName { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int FacultyDetailsId { get; set; }

        public IEnumerable<SelectListItem> FacultyOptions { get; set; }

        [Required(ErrorMessage = "Fees is required")]
        public int Fees { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        

        public FacultyDetails FacultyDetails { get; set; }

        public ICollection<EnrollDetails> Enroll { get; set; }
    }
}
