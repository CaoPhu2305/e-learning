using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ORDER_STATUS")]
    public class OrderStatus
    {


        [Key, Column("ORDER_STATUS_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal OrderStatusID { get; set; }


        [Required]
        [MaxLength(50)]
        [Column("ORDER_STATUS_NAME")]
        public  string OrderStatusName { get; set; }    

        public virtual ICollection<Order> Orders { get; set; }

        public OrderStatus(string orderStatusName)
        {
            OrderStatusName = orderStatusName;
        }

        public OrderStatus()
        {
        }
    }
}
