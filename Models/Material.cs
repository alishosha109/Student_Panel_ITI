using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Panel_ITI.Models
{
    [Table("Material")]
    public class Material
    {
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }


        [Required, MaxLength(50)]
        public string Type { get; set; }



        [ForeignKey(nameof(Instructor))]
        public string? InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }


        public virtual IEnumerable<Course_Day_Material>? CourseDayMaterials { set; get; }
    }
}