using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("INTERACTIVE_CLASS")]

    public class InteractiveClass
    {


        [Key, ForeignKey("Course")]
        [Column("INTERACTIVE_CLASS_ID")]
        public decimal InteractiveClassID { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("INTERACTIVE_CLASS_NAME")]
        public string InteractiveName { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        public InteractiveClass(decimal interactiveClassID, string interactiveName)
        {
            InteractiveClassID = interactiveClassID;
            InteractiveName = interactiveName;
        }

        public InteractiveClass()
        {
        }
    }
}
