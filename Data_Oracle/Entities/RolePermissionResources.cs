using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ROLE_PERMISSION_RESOURCES")]
    public class RolePermissionResources
    {

        [Key, Column(Order = 0)]
        
        public decimal PermissionID { get; set; }


        [Key, Column(Order = 1)]
        
        public decimal RoleID { get; set; }


        [Key, Column(Order = 2)]
      
        public decimal ResourcesID { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        [ForeignKey("PermissionID")]
        public virtual Permission Permission { get; set; }

        [ForeignKey("ResourcesID")]
        public virtual Resources Resources { get; set; }


    }
}
