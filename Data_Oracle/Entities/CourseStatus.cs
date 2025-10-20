using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("COURSE_STATUS")]
    public class CourseStatus
    {

        [Key]
        [Column("COURSE_STATUS_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal CourseStatusID { get; set; }


        [Required]
        [Column("COURSE_STATUS_NAME")]
        [MaxLength(50)]
        public string CourseStatusName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public CourseStatus(string courseStatusName)
        {
            CourseStatusName = courseStatusName;
        }

        public CourseStatus()
        {
        }
    }
}
