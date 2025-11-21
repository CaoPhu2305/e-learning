using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("USER_ANSWERS")]
    public class UserAnswers
    {
        [Key]
        [Column("USER_ANSWERS_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal UserAnswersID { get; set; }

        [Column("ANSWER_OPTIONS_ID")]
        [ForeignKey(nameof(AnswerOptions))]
        public decimal AnswerOptionsID { get; set; }

        [Column("QUESTIONS_ID")]
        [ForeignKey(nameof(Questions))]
        public decimal QuestionsID { get; set; }

        [Column("QUIZ_ATTEMPT_ID")]
        [ForeignKey(nameof(QuizAttempt))]
        public decimal QuizAttemptID { get ; set; }

        public virtual AnswerOptions AnswerOptions { get; set; }

        public virtual Questions Questions { get; set; }
        
        public virtual QuizAttempt QuizAttempt { get; set; }

        public UserAnswers(decimal answerOptionsID, decimal questionsID, decimal quizAttemptID)
        {
            AnswerOptionsID = answerOptionsID;
            QuestionsID = questionsID;
            QuizAttemptID = quizAttemptID;
        }

        public UserAnswers(decimal quizAttemptID, decimal questionsID, decimal answerOptionsID, decimal userID)
        {
            AnswerOptionsID = answerOptionsID;
            QuestionsID = questionsID;
            QuizAttemptID = quizAttemptID;
            UserAnswersID = userID;
        }

        public UserAnswers()
        {
        }
    }
}
