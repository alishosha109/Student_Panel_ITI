using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Student_Course")]
    public class Student_Course
    {
        //old//
        [ForeignKey(nameof(Student))]
        public string StudentID { get; set; }
        public virtual Student? Student { get; set; }


        //new//
        //[ForeignKey(nameof(Student))]
        //public string StudentID { get; set; }
        //public virtual AppUser? Student { get; set; }



        [ForeignKey(nameof(Course))]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }
    }
}
