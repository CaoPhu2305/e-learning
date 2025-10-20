using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("QUESTIONS")]
    public class Questions
    {
        [Key]
        [Column("QUESTIONS_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal QuestionsID { get; set; }

        [Required]
        [ForeignKey(nameof(Quizzes))]
        [Column("QUIZZES_ID")]
        public decimal QuizzesID { get; set; }


        [Required]
        [Column("QUESTIONS_CONTENT")]
        [MaxLength(50)]
        public string QuestionsContent { get; set; }

        //[ForeignKey(nameof(Quizzes))]
        public virtual Quizzes Quizzes { get; set; }

        public virtual ICollection<AnswerOptions> AnswerOptions { get; set; }

        public virtual ICollection<UserAnswers> UserAnswers { get; set; }

        public Questions(decimal quizzesID, string questionsContent)
        {
            QuizzesID = quizzesID;
            QuestionsContent = questionsContent;
        }

        public Questions()
        {
        }
    }
}
