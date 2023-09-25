using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Student_Submission")]
    public class Student_Submission
    {
        [Required]
        public string SubmissionPath { get; set; }
        public double SubmissionGrade { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime Date { get; set; }




        [ForeignKey(nameof(CourseDay))]
        public int CourseDayID { get; set; }
        public virtual CourseDay? CourseDay { get; set; }


        //old//
        [ForeignKey(nameof(Student))]
        public string StudentID { get; set; }
        public virtual Student? Student { get; set; }


        //new//
        //[ForeignKey(nameof(Student))]
        //public string StudentID { get; set; }
        //public virtual AppUser? Student { get; set; }
    }
}
