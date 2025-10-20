using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ANSWER_OPTIONS")]
    public class AnswerOptions
    {

        [Key]
        [Column("ANSWER_OPTIONS_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal AnswerOptionsID { get; set; }

        [Required]
        [Column("ANSWER_OPTIONS_NAME")]
        [MaxLength(50)]
        public string AnswerOptionsName { get; set; }

        [Required]
        [Column("IS_CORRECT")]
        public Boolean IsCorrect { get; set; }

        [Required]
        [Column("QUESTIONS_ID")]
        [ForeignKey(nameof(Questions))]
        public decimal QuestionsID { get; set; }


        public virtual Questions Questions { get; set; }

        public virtual ICollection<UserAnswers> UserAnswers { get; set; }

        public AnswerOptions(string answerOptionsName, bool isCorrect, decimal questionsID)
        {
            AnswerOptionsName = answerOptionsName;
            IsCorrect = isCorrect;
            QuestionsID = questionsID;
        }

        public AnswerOptions()
        {
        }
    }
}
