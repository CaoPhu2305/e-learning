using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("CHAPTER")]
    public class Chapter
    {
        [Key]
        [Column("CHAPTER_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal ChapterID { get; set; }

        [Required]
        [Column("COURSE_VIDEO_ID")]
        [ForeignKey(nameof(CourseVideo))]
        public decimal CourcesVideoID { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("CHAPTER_NAME")]
        public string ChapterName { get; set; }

        [Required]
        [Column("CHAPTER_INDEX")]
        public int ChapterIndex { get; set; }

        [Required]
        [Column("CHAPTER_COMPLATED")]
        [MaxLength(50)]
        public string ChapterComplated { get; set; }

        public virtual CourseVideo CourseVideo { get; set; }

        public virtual ICollection<Lession> Lessions { get; set; }

        public virtual ICollection<Quizzes> Quizzes { get; set; }

        public Chapter(decimal courcesVideoID, string chapterName, int chapterIndex)
        {
            CourcesVideoID = courcesVideoID;
            ChapterName = chapterName;
            ChapterIndex = chapterIndex;
         
        }

        public Chapter()
        {
        }
    }
}
