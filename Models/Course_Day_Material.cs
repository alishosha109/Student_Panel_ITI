using Student_Panel_ITI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Panel_ITI.Models
{
    [Table("Course_Day_Material")]
    public class Course_Day_Material
    {
        [ForeignKey(nameof(Course))]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }



        [ForeignKey(nameof(CourseDay))]
        public int CourseDayID { get; set; }
        public virtual CourseDay? CourseDay { get; set; }



        [ForeignKey(nameof(MaterialOfDay))]
        public int MaterialID { get; set; }
        public virtual Material? MaterialOfDay { get; set; }
    }
}
