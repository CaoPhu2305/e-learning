using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("USERS")]
    public class User
    {
        [Key]
        [Column("USER_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal UserID { get; set; }

        [Required]
        [Column("USER_NAME")]
        [MaxLength(50)]
        public String UserName { get; set; }

        [Required]
        [Column("EMAIL")]
        [MaxLength (80)]
        public String Email { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("HASHPASSWORD")]
        public String HashPassword { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        public User(string userName, string email, string hashPassword)
        {
            UserName = userName;
            Email = email;
            HashPassword = hashPassword;
        }

        public User()
        {
        }
    }
}
