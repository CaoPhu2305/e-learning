using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("LESSION")]
    public class Lession
    {

        [Key]
        [Column("LESSION_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal LessionID { get; set; }

        [Required]
        [Column("LESSION_NAME")]
        [MaxLength(50)]
        public string LessionName { get; set; }

        [Required]
        [Column("CHAPTER_ID")]
        [ForeignKey(nameof(Chapter))]
        public decimal ChapterID { get; set; }

        [Column("VIDEO_DATA")]
        public byte[] VideoData { get; set; }

        //[ForeignKey(nameof(Chapter))]
        public virtual Chapter Chapter { get; set; }

        public virtual ICollection<Quizzes> Quizzes { get; set; }

        public Lession(string lessionName, decimal chapterID, byte[] videoData)
        {
            LessionName = lessionName;
            ChapterID = chapterID;
            VideoData = videoData;
        }

        public Lession()
        {
        }
    }
}
