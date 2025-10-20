using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ENROLLMENTS")]
    public class Enrollments
    {
        [Key]
        [Column("ENROLLMENTS_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal EnrollmentsID { get; set; }

        [Required]
        [Column("ENROLLMENTS_NAME")]
        [MaxLength(50)]
        public string EnrollmentsName { get; set; }

        [Required]
        [Column("USER_ID")]
        [ForeignKey(nameof(User))]
        public decimal UserID { get; set; }

        [Required]
        [Column("ENROLLMENT_STATUS_ID")]
        [ForeignKey(nameof(EnrollmentStatus))]
        public decimal EnrollmentStatusID { get; set; }

        [Required]
        [Column("COURSE_ID")]
        [ForeignKey(nameof(Course))]
        public decimal CourseID { get; set; }

        [Required]
        [Column("ENROLLMENTS_DATE")]
        public DateTime EnrollmentsDate { get; set; }

       
        public virtual User User { get; set; }

       
        public virtual Course Course { get; set; }


        public virtual EnrollmentStatus EnrollmentStatus { get; set; }

        public Enrollments(string enrollmentsName, decimal userID, decimal enrollmentStatusID, decimal courseID, DateTime enrollmentsDate)
        {
            EnrollmentsName = enrollmentsName;
            UserID = userID;
            EnrollmentStatusID = enrollmentStatusID;
            CourseID = courseID;
            EnrollmentsDate = enrollmentsDate;
        }

        public Enrollments()
        {
        }
    }
}
