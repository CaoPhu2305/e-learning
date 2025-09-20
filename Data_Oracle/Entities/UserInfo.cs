using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("USER_INFO")]
    public class UserInfo
    {

        [Key, ForeignKey("User")]
        [Column("USER_ID")]
        public decimal UserID { get; set; }

        [Column("ADDRESS")]
        [Required]
        [MaxLength(50)]
        public string Address { get; set; }


        [Required]
        [MaxLength(10)]
        [Column("PHONE")]
        public string Phone { get; set; }


        [Required]
        [MaxLength(10)]
        [Column("GENDER")]
        public string Gender { get; set; }

       
        public virtual User User { get; set; }

    }
}
