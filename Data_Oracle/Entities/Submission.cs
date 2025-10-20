using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("SUBMISSION")]
    public class Submission
    {
        [Key]
        [Column("SUBMISSION_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal SubmissionID { get; set; }
        
        [Column("ASSIGNMENT_ID")]
        [ForeignKey(nameof(Assignment))]
        public decimal AssignmentID { get; set; }

        [Column("USER_ID")]
        [ForeignKey(nameof(User))]
        public decimal UserID { get; set; }

        [Column("FILE")]
        public byte[] File { get; set; }

        [Column("TIME_SUBMISSION")]
        public DateTime TimeSubmission { get; set; }

        //[ForeignKey(nameof(User))]
        public virtual User User { get; set; }

        //[ForeignKey(nameof(Assignment))]
        public virtual Assignment Assignment { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }

        public Submission(decimal assignmentID, decimal userID, byte[] file, DateTime timeSubmission)
        {
            AssignmentID = assignmentID;
            UserID = userID;
            File = file;
            TimeSubmission = timeSubmission;
        }

        public Submission()
        {
        }
    }
}
