using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("USER_ROLE")]
    public class UserRole
    {

        [Key, Column("USER_ID", Order = 0)]
        [ForeignKey(nameof(User))]
        
        public decimal UserID { get; set; }

        [Key, Column("ROLE_ID", Order = 1)]
        [ForeignKey(nameof(Role))]
        public decimal RoleID { get; set; }

       
        public virtual User User { get; set; }

     
        public virtual Role Role { get; set; }

        public UserRole(decimal userID, decimal roleID)
        {
            UserID = userID;
            RoleID = roleID;
        }

        public UserRole()
        {
        }
    }
}
