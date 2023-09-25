//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace Student_Panel_ITI.Models
//{
//    [Table("Intake_Track")]
//    public class Intake_Track
//    {
//        [Range(10, 25)]
//        public int NumOfStdsInTrack { get; set; }



//        [ForeignKey(nameof(Intake))]
//        public int IntakeID { get; set; }
//        public virtual Intake? Intake { get; set; }



//        [ForeignKey(nameof(Track))]
//        public int TrackID { get; set; }
//        public virtual Track? Track { get; set; }
//    }
//}
