using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("COURSE_VIDEO")]
    public class CourseVideo
    {

        [Key, ForeignKey("Course")]
        [Column("COURSE_VIDEO_ID")]
        public decimal CourseVideoID { get; set; }

        [Required]
        [Column("COURSE_VIDEO_NAME")]
        [MaxLength(50)]
        public string CourcesVideoName { get ; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }

        public CourseVideo(decimal courseVideoID, string courcesVideoName)
        {
            CourseVideoID = courseVideoID;
            CourcesVideoName = courcesVideoName;
        }

        public CourseVideo()
        {
        }
    }
}
