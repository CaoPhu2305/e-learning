using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("ORDER_DETAIL")]
    public class OrderDetail
    {

        [Key, Column("ORDER_DETAIL_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal OrderDetailID { get; set; }

        [Column("ORDER_ID")]
        [ForeignKey(nameof(Order))]
        public decimal OderID { get; set; }
        
        [Column("COURSE_ID")]
        [ForeignKey(nameof(Course))]
        public decimal CourseID { get; set; }


        [Required]
        [Column("PRICE_AT_PURCHASE")]
        public double PriceAtPurchase { get; set; }

        
        [Column("PROMOTION_ID")]
        [ForeignKey(nameof(Promotion))]
        public decimal? PromotionID {get; set; }


        //[ForeignKey(nameof(Order))]
        public virtual Order Order { get; set; }

        //[ForeignKey(nameof(Course))]
        public virtual Course Course { get; set; }


        //[ForeignKey(nameof(Promotion))]
        public virtual Promotion Promotion { get; set; }

        public OrderDetail(decimal oderID, decimal courseID, double priceAtPurchase, decimal? promotionID)
        {
            OderID = oderID;
            CourseID = courseID;
            PriceAtPurchase = priceAtPurchase;
            PromotionID = promotionID;
        }

        public OrderDetail(decimal orderDetaiID, decimal oderID, decimal courseID, double priceAtPurchase, decimal? promotionID)
        {
            OderID = oderID;
            CourseID = courseID;
            PriceAtPurchase = priceAtPurchase;
            PromotionID = promotionID;
            OrderDetailID = orderDetaiID;
        }

        public OrderDetail() { }

    }
}
