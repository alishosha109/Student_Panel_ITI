using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Track")]
    public class Track
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }


        //old//
        [ForeignKey(nameof(Manager))]
        public string? ManagerID { get; set; }  //InstructorID, string because it will be GUID (hashed password)
        public virtual Instructor? Manager { get; set; }


        //new//
        //[ForeignKey(nameof(Manager))]
        //public string? ManagerID { get; set; }  //InstructorID, string because it will be GUID (hashed password)
        //public virtual AppUser? Manager { get; set; }



        [ForeignKey(nameof(Admin))]
        public string? AdminID { get; set; }
        public virtual Admin? Admin { get; set; }


        public virtual IEnumerable<Intake_Track_Course>? IntakeTrackCourse { get; set; }

    }
}
