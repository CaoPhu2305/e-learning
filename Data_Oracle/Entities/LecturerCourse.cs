using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{

    [Table("LECTURE_COURSE")]
    public class LecturerCourse
    {
        
        [Key, Column("USER_ID", Order = 1)]
        [ForeignKey(nameof(User))]
        public decimal UserID { get; set; }

        [Key, Column("COURSE_ID", Order = 2)]
        [ForeignKey(nameof(Course))]
        public decimal CourseID { get; set; }

        [Required]
        [Column("CREATE_AT_TIME")]
        public DateTime CreateAtTime { get; set; }

        //[ForeignKey(nameof(User))]
        public virtual User User { get; set; }

        //[ForeignKey(nameof(Course))]
        public virtual Course Course { get; set; }

        public LecturerCourse(decimal userID, decimal courseID, DateTime createAtTime)
        {
            UserID = userID;
            CourseID = courseID;
            CreateAtTime = createAtTime;
        }

        public LecturerCourse()
        {
        }
    }
}
