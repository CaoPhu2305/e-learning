using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("GRADE")]
    public class Grade
    {

        [Key]
        [Column("GRADE_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal GradeID { get; set; }

        [Column("USER_ID")]
        [ForeignKey(nameof(User))]
        public decimal UserID { get; set; }


        [Column("SUBMISSION_ID")]
        [ForeignKey(nameof(Submission))]
        public decimal SubmissionID { get; set; }

        [Column("SCORE")]
        public decimal Score { get; set; }

        [Column("FEED_BACK")]
        public string Feedback { get; set; }

        public virtual User User { get; set; }

        public virtual Submission Submission { get; set; }

        public Grade(decimal userID, decimal submissionID, decimal score, string feedback)
        {
            UserID = userID;
            SubmissionID = submissionID;
            Score = score;
            Feedback = feedback;
        }

        public Grade()
        {
        }
    }
}
