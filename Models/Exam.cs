using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Exam")]
    public class Exam
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        public int Duration { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }



        [ForeignKey(nameof(Instructor))]
        public string? InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }



        [ForeignKey(nameof(Course))]
        public int? CourseID { get; set; }
        public virtual Course? Course { get; set; }


        public virtual IEnumerable<Exam_Std_Question>? Student_Quest_Exam { get; set; }
        public virtual IEnumerable<Exam_Question>? Exam_Question { get; set; }

    }
}
