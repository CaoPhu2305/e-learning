using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("QUIZZES")]
    public class Quizzes
    {

        [Key]
        [Column("QUIZZES_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal QuizzesID { get; set; }

        [Required]
        [Column("QUIZZES_NAME")]
        [MaxLength(50)]
        public string QuizzesName { get; set; }

        [Required]
        [Column("QUIZZES_TYPE")]
        [MaxLength(50)]
        public string Quizzes_Type { get; set; }

        [Required]
        [Column("PASS_SCORE_PERCENT")]
        public float Pass_Score_Percent { get; set; }

        [Required]
        [Column("CHAPTER_ID")]
        [ForeignKey(nameof(Chapter))]
        public decimal ChapterID { get; set; }
       
        [Required]
        [Column("DUE_DATE")]
        public DateTime DueDate { get; set; }

        [Required]
        [Column("NUMBER_QUESTIONS")]
        public decimal NUMBER_QUESTIONS { get; set; }

        [Required]
        [Column("TIME_LIMIT")]
        public decimal TimeLimit { get; set; }



        //[ForeignKey(nameof(Lession))]
        public virtual Chapter Chapter { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }

        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }

        public Quizzes(string quizzesName, string quizzes_Type, float pass_Score_Percent, decimal chapterID)
        {
            QuizzesName = quizzesName;
            Quizzes_Type = quizzes_Type;
            Pass_Score_Percent = pass_Score_Percent;
            ChapterID = chapterID;
        }

        public Quizzes()
        {


        }
    }
}
