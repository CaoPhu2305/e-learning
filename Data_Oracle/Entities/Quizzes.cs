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
        [Column("LESSION_ID")]
        [ForeignKey(nameof(Lession))]
        public decimal LessionID { get; set; }

        //[ForeignKey(nameof(Lession))]
        public virtual Lession Lession { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }

        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }

        public Quizzes(string quizzesName, string quizzes_Type, float pass_Score_Percent, decimal lessionID)
        {
            QuizzesName = quizzesName;
            Quizzes_Type = quizzes_Type;
            Pass_Score_Percent = pass_Score_Percent;
            LessionID = lessionID;
        }

        public Quizzes()
        {
        }
    }
}
