using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Oracle.Entities
{
    [Table("COURSE")]
    public class Course
    {
        [Key]
        [Column("COURSE_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal CourseID { get; set; }

        [Column("COURSE_TYPE_ID")]
        [ForeignKey(nameof(CourseType))]
        public decimal CourseTypeID { get; set; }

        [Column("COURSE_STATUS_ID")]
        [ForeignKey(nameof(CourseStatus))]
        public decimal CourseStatusID { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("COURSE_NAME")]
        public string CourseName { get; set; }

        [Required]
        [Column("PRICE")]
        public double Price { get; set; }
            
        [Required]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        //[Required]
        //[Column("IMAGE_NAME")]
        //public string Image { get; set; }



        // nếu chia ra video và kèm 1 1 hay lớp nhiều người thêm type vào 
          
        public virtual CourseStatus CourseStatus { get; set; }

        public virtual CourseType CourseType { get; set; }

        public virtual ICollection<LecturerCourse> LecturerCourses { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        // 1 - 1
        public virtual CourseVideo Video { get; set; }
        // 1 - 1
        public virtual InteractiveClass InteractiveClass { get; set; }

        public virtual ICollection<CoursePromotion> CoursePromotions { get; set; }

        public virtual ICollection<Enrollments> Enrollments { get; set; }

        public Course(decimal courseTypeID, decimal courseStatusID, string courseName, double price, string description)
        {
            CourseTypeID = courseTypeID;
            CourseStatusID = courseStatusID;
            CourseName = courseName;
            Price = price;
            Description = description;
        }

        public Course()
        {

        }
    }
}
