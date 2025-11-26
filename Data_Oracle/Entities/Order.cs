using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{

    [Table("ORDERS")]
    public class Order
    {

        [Key, Column("ORDER_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal OrderID { get; set; }

        [Column("USER_ID")]
        [ForeignKey(nameof(User))]
        public decimal UserID { get; set; }

        [Column("ORDER_STATUS_ID")]
        [ForeignKey(nameof(OrderStatus))]
        public decimal OrderStatusID { get; set; }

        // khóa ngoại là User

        //[ForeignKey(nameof(User))]
        public virtual User User { get; set; }

        //[ForeignKey(nameof(OrderStatus))]
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order(decimal userID, decimal orderStatusID)
        {
            UserID = userID;
            OrderStatusID = orderStatusID;
        }

        public Order()
        {
        }
    }
}
