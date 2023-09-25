using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Admin")]
    public class Admin 
    {
        [ForeignKey(nameof(AspNetUser))]
        [Key]
        public string? AspNetUserID { get; set; }
        public virtual AppUser? AspNetUser { get; set; }




        public virtual IEnumerable<Intake>? Intakes { get; set; }
        public virtual IEnumerable<Track>? Tracks { get; set; }
     

    }
}
