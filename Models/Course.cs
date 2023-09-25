using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Duration { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }






        //old//
        [ForeignKey(nameof(Admin))]
        public string? AdminID { get; set; }
        public virtual Admin? Admin { get; set; }


        //new//
        //[ForeignKey(nameof(Admin))]
        //public string AdminID { get; set; }
        //public virtual AppUser? Admin { get; set; }


        public virtual IEnumerable<Exam>? Exams { get; set; }


        //Many to Many navigation properties//
        public virtual IEnumerable<Student_Course>? StudentCourses { get; set; }
        public virtual IEnumerable<Instructor_Course>? InstructorCourses { get; set; }
        public virtual IEnumerable<Course_Day_Material>? CourseDayMaterials { set; get; }
        public virtual IEnumerable<Intake_Track_Course>? IntakeTrackCourse { get; set; }


    }
}
