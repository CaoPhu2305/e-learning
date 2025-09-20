using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ROLE")]
    public class Role
    {

        [Key]
        [Column("ROLE_ID")]
        public decimal RoleID { get; set; }

        [Required]
        [Column("ROLE_NAME")]
        [MaxLength(15)]
        public string RoleName { get; set; }


        [Required]
        [Column("ROLE_DESCRIPTION")]
        [MaxLength(30)]
        public string RoleDescription { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

        public virtual ICollection<RolePermissionResources> RolePermissionResources { get; set; }


    }
}
