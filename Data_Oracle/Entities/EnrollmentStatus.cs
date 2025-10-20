using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ENROLLMENT_STATUS")]
    public class EnrollmentStatus
    {
        [Key]
        [Column("ENROLLMENT_STATUS_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal EnrollmentStatusID { get; set; }

        [Required]
        [Column("ENROLLMENT_STATUS_NAME")]
        [MaxLength(50)]
        public string EnrollmentStatusName { get; set; }

        public virtual ICollection<Enrollments> Enrollments { get; set; }

        public EnrollmentStatus(string enrollmentStatusName)
        {
            EnrollmentStatusName = enrollmentStatusName;
        }

        public EnrollmentStatus()
        {
        }
    }
}
