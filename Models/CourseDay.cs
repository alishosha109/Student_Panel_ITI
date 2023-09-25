using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("CourseDay")]
    public class CourseDay
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int DayNumber { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        public string TaskPath { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime DeadLine { get; set; }




        public virtual IEnumerable<Course_Day_Material>? CourseDayMaterials { set; get; }

        public virtual IEnumerable<Student_Submission>? StudentsSubmissions { set; get; }

    }
}
