using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("COURSE_TYPE")]
    public class CourseType
    {

        [Key]
        [Column("COURSE_TYPE_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal CourcesTypeID { get; set; }

        [Required]
        [Column("COURSE_TYPE_NAME")]
        [MaxLength(30)]
        public string CourseTypeName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public CourseType(string courseTypeName)
        {
            CourseTypeName = courseTypeName;
          
        }

        public CourseType()
        {
        }
    }
}
