using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{ 
    public class Intake_Instructor
    {

        [ForeignKey(nameof(Intake))]
        public int IntakeID { get; set; }
        public virtual Intake? Intake { get; set; }


        //old//
        [ForeignKey(nameof(Instructor))]
        public string InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }


        //[ForeignKey(nameof(Instructor))]
        //public string InstructorID { get; set; }
        //public virtual AppUser? Instructor { get; set; }
    }
}
