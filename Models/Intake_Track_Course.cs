using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    public class Intake_Track_Course
    {

        public int numOfStudentsInCourse { get; set; }

        [ForeignKey(nameof(Intake))]
        public int IntakeID { get; set; }
        public virtual Intake? Intake { get; set; }



        [ForeignKey(nameof(Track))]
        public int TrackID { get; set; }
        public virtual Track? Track { get; set; }



        [ForeignKey(nameof(Course))]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }

    }
}
