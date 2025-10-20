using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ATTENDANCE")]
    public class Attendance
    {
        [Key]
        [Column("ATTENDANCE_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal AttendanceID { get; set; }
        
        [Column("USER_ID")]
        [ForeignKey(nameof(User))]
        public decimal UserID { get; set; }

        [Column("SESSION_ID")]
        [ForeignKey(nameof(Session))]
        public decimal SessionID { get ; set; }

        [Column("TIME_JOIN")]
        public DateTime TimeJoin { get; set; }

        public virtual User User { get; set; }

        public virtual Session Session { get; set; }

        public Attendance(decimal userID, decimal sessionID, DateTime timeJoin)
        {
            UserID = userID;
            SessionID = sessionID;
            TimeJoin = timeJoin;
        }

        public Attendance() { }

    }
}
