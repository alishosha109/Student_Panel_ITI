using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Instructor_Course")]
    public class Instructor_Course
    {
        [ForeignKey(nameof(Course))]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }



        //old//
        [ForeignKey(nameof(Instructor))]
        public string InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }


        //new//
        //[ForeignKey(nameof(Instructor))]
        //public string InstructorID { get; set; }
        //public virtual AppUser? Instructor { get; set; }
    }
}
