using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Instructor")]
    public class Instructor
    {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }




        public bool CurrentlyWorking { get; set; } = true;




        [ForeignKey(nameof(AspNetUser))]
        [Key]
        public string? AspNetUserID { get; set; }
        public virtual AppUser? AspNetUser { get; set; }





        [ForeignKey(nameof(Admin))]
        public string? AdminID { get; set; }
        public virtual AppUser? Admin { get; set; }



        public virtual IEnumerable<Track>? Tracks { get; set; }
        public virtual IEnumerable<Exam>? Exams { get; set; }


        public virtual IEnumerable<Instructor_Course>? InstructorCourses { get; set; }
        public virtual IEnumerable<Intake_Instructor>? IntakeInstructors { get; set; }
        public virtual IEnumerable<Material>? Materials { get; set; }
    }
}
