using Student_Panel_ITI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Panel_ITI.Models
{
    [Table("Std_Quest_Exam")]
    public class Exam_Std_Question
    {
        [MaxLength(100)]
        public string StudentAnswer { get; set; }

        [Required]
        public int StudentGrade { get; set; }





        [ForeignKey(nameof(Student))]
        public string StudentID { get; set; }
        public virtual Student? Student { get; set; }



        [ForeignKey(nameof(Exam))]
        public int ExamID { get; set; }
        public virtual Exam? Exam { get; set; }



        [ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }
        public virtual Question? Question { get; set; }
    }
}
