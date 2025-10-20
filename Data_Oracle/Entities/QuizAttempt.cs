using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("QUIZ_ATTEMPT")]
    public class QuizAttempt
    {
        [Key]
        [Column("QUIZ_ATTEMPT_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal QuizAttemptID { get; set; }

        [Column("QUIZZES_ID")]
        [ForeignKey(nameof(Quizzes))]
        public decimal QuizzesID { get; set; }

        [Column("USER_ID")]
        [ForeignKey(nameof(User))]
        public decimal UserID { get; set; }
        
        
        [Column("QUIZ_ATTEMPT_DATE")]        
        
        public DateTime AttemptDate { get; set; }

        [Column("QUIZ_ATTEMPT_SCORE")]
        public float Score { get; set; }

        [Column("QUIZ_ATTEMPT_IS_PASS")]
        public Boolean isPass { get ; set; }

        //[ForeignKey(nameof(User))]
        public virtual User User { get; set; }


        //[ForeignKey(nameof(Quizzes))]
        public virtual Quizzes Quizzes { get; set; }    

        public virtual ICollection<UserAnswers> UserAnswers { get; set; }

        public QuizAttempt(decimal quizzesID, decimal userID, DateTime attemptDate, float score, bool isPass)
        {
            QuizzesID = quizzesID;
            UserID = userID;
            AttemptDate = attemptDate;
            Score = score;
            this.isPass = isPass;
        }

        public QuizAttempt()
        {
        }
    }
}
