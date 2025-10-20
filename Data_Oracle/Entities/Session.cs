using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("SESSION")]
    public class Session
    {
        [Key]
        [Column("SESSION_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal SessionID { get; set; }

        [Required]
        [Column("SESSION_NAME")]
        [MaxLength(50)]
        public string SessionName { get; set; }

        [Required]
        [Column("START_TIME")]
        public DateTime StartTime { get; set; }

        [Required]
        [Column("END_TIME")]
        public DateTime EndTime { get; set; }

        [Required]
        [Column("MEETING_LINK")]
        public string MeetingLink { get; set; }

        [Column("INTERACTIVE_CLASS_ID")]
        [ForeignKey(nameof(InteractiveClass))]
        public decimal InteractiveClassID { get; set; }

        //[ForeignKey(nameof(InteractiveClass))]
        public virtual InteractiveClass InteractiveClass { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        public Session(string sessionName, DateTime startTime, DateTime endTime, string meetingLink, decimal interactiveClassID)
        {
            SessionName = sessionName;
            StartTime = startTime;
            EndTime = endTime;
            MeetingLink = meetingLink;
            InteractiveClassID = interactiveClassID;
        }

        public Session()
        {
        }
    }
}
