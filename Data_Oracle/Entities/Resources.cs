using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{

    [Table("RESOURCES")]
    public class Resources
    {

        [Key]
        [Column("RESOURCES_ID")]
        public decimal ResourcesID { get; set; }


        [Required]
        [Column("RESOURCES_NAME")]
        [MaxLength(20)]
        
        public string ResourcesName { get; set; }


        [Required]
        [Column("RESOURCES_DESCRIPTION")]
        public string ResourcesDescription { get; set; }


        public virtual ICollection<RolePermissionResources> RolePermissionResources { get; set; }

    }
}
