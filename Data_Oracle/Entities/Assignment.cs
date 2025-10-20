using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ASSIGNMENT")]
    public class Assignment
    {
        [Key]
        [Column("ASSIGNMENT_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal AssignmentID { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ASSIGNMENT_NAME")]
        public string AssignmentName {get; set; }

        [Required]
        [Column("DEAD_LINE")]
        public DateTime DeadLine {  get; set; }

        [Column("CREATE_DATE")]
        public DateTime CreatedDate { get; set; }

        [Column("SESSION_ID")]
        [ForeignKey(nameof(Session))]
        public decimal SessionID { get; set; }

        public virtual Session Session { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }

        public Assignment()
        {

        }

        public Assignment(string assignmentName, DateTime deadLine, decimal sessionID)
        {
            AssignmentName = assignmentName;
            DeadLine = deadLine;
            SessionID = sessionID;
        }
    }
}
