using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("PROMOTION")]
    public class Promotion
    {


        [Key]
        [Column("PROMOTION_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal PromotionID { get; set; }

        [Required]
        [Column("PROMOTION_NAME")]
        [MaxLength(30)]
        public string PromotionName { get; set; }

        [Required]
        [Column("DISCOUNT_VALUE")]
        public decimal DiscountValue { get; set; }

        [Required]
        [Column("START_DATE")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("END_DATE")]
        public DateTime EndDate { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<CoursePromotion> CoursePromotions { get; set; }

        public Promotion(string promotionName, decimal discountValue, DateTime startDate, DateTime endDate)
        {
            PromotionName = promotionName;
            DiscountValue = discountValue;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Promotion()
        {
        }
    }
}
