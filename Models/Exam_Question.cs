using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Exam_Question")]
    public class Exam_Question
    {
        [ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }
        public virtual Question? Question { get; set; }


        [ForeignKey(nameof(Exam))]
        public int ExamID { get; set; }
        public virtual Exam? Exam { get; set; }

    }
}
