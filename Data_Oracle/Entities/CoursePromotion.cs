using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{

    [Table("COURSE_PROMOTION")]
    public class CoursePromotion
    {

        [Key, Column("COURSE_ID", Order =0)]
        [ForeignKey(nameof(Course))]
        public decimal CourseID { get; set; }


        [Key, Column("PROMOTION_ID", Order = 1)]
        [ForeignKey(nameof(Promotion))]
        public decimal PromotionID { get; set; }

        //[ForeignKey(nameof(User))] để  ý
        public virtual Course Course { get; set; }

        //[ForeignKey(nameof(Promotion))] để ý
        public virtual Promotion Promotion { get; set; }

        public CoursePromotion(decimal courseID, decimal promotionID)
        {
            CourseID = courseID;
            PromotionID = promotionID;
        }

        public CoursePromotion()
        {
        }
    }
}
