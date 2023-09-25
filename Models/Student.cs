using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Student")]
    public class Student 
    {
        public bool IsGraduated { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }






        [ForeignKey(nameof(Intake))]
        public int IntakeID { get; set; }
        public virtual Intake? Intake { get; set; }



        [ForeignKey(nameof(Track))]
        public int TrackID { get; set; }
        public virtual Track? Track { get; set; }



        [ForeignKey(nameof(AspNetUser))]
        [Key]
        public string? AspNetUserID { get; set; }
        public virtual AppUser? AspNetUser { get; set; }




        [ForeignKey(nameof(Admin))]
        public string? AdminID { get; set; }
        public virtual AppUser? Admin { get; set; }



        //Many to Many navigation properties//
        public virtual IEnumerable<Student_Course>? StudentCourses { get; set; }
        public virtual IEnumerable<Exam_Std_Question>? Student_Quest_Exam { get; set; }
        public virtual IEnumerable<Student_Submission>? StudentsSubmissions { set; get; }
    }
}
