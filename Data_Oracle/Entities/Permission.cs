using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Oracle.Entities
{
    [Table("PERMISSION")]
    public class Permission
    {

        [Key]
        [Column("PERMISSION_ID")]
       
        public decimal PermissionID { get; set; }


        [Required]
        [MaxLength(30)]
        [Column("PERMISSION_NAME")]
        public string PermissionName { get; set; }

        [Required]
        [Column("PERMISSION_DESCRIPTION")]
        [MaxLength(50)]
        public string PermissionDescription { get; set; }

        public virtual ICollection<RolePermissionResources> RolePermissionResources { get; set; }

    }
}
